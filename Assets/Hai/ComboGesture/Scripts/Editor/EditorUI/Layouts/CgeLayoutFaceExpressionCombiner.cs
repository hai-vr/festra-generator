﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hai.ComboGesture.Scripts.Components;
using Hai.ComboGesture.Scripts.Editor.Internal;
using UnityEditor;
using UnityEngine;

namespace Hai.ComboGesture.Scripts.Editor.EditorUI.Layouts
{
    public class CombinerState
    {
        public CgeActivityEditorCombiner Combiner;
        public bool ComplexCombiner;
        public bool ShowFullNames;
        public string CombinerTarget;
        public string CombinerCandidateFileName;
        public bool CombinerIsLikelyEyesClosed;
        public bool CombinerIsAPermutation;

        public void DoSetCombiner(ComboGestureActivity activity, AnimationClip leftAnim, AnimationClip rightAnim, string propertyPath, bool usePermutations, Action repaintCallback, CgeEditorHandler editorHandler, EeRenderingCommands previewController)
        {
            Combiner = new CgeActivityEditorCombiner(leftAnim, rightAnim, repaintCallback, editorHandler /* FIXME: it is not normal to pass the Handler here*/, previewController);
            Combiner.Prepare();

            CombinerTarget = propertyPath;
            CombinerIsAPermutation = usePermutations;
            CombinerCandidateFileName = "cge_" + leftAnim.name + "__combined__" + rightAnim.name;

            CombinerIsLikelyEyesClosed = activity.blinking.Contains(leftAnim) || activity.blinking.Contains(rightAnim);
        }
    }

    public class CgeLayoutFaceExpressionCombiner
    {
        private readonly CgeLayoutCommon _common;
        private readonly CgeActivityEditorDriver _driver;
        private readonly CombinerState _combinerState = new CombinerState();
        private readonly CgeEditorHandler _editorHandler;
        private readonly EeRenderingCommands _renderingCommands;

        public CgeLayoutFaceExpressionCombiner(CgeLayoutCommon common, CgeActivityEditorDriver driver, CgeEditorHandler editorHandler, EeRenderingCommands renderingCommands)
        {
            _common = common;
            _driver = driver;
            _editorHandler = editorHandler;
            _renderingCommands = renderingCommands;
        }

        public void DoSetCombiner(AnimationClip leftAnim, AnimationClip rightAnim, String propertyPath, bool usePermutations, Action repaintCallback)
        {
            _combinerState.DoSetCombiner(_editorHandler.GetActivity(), leftAnim, rightAnim, propertyPath, usePermutations, repaintCallback, _editorHandler, _renderingCommands);

            _editorHandler.SwitchAdditionalEditorTo(AdditionalEditorsMode.CombineFaceExpressions);
            _editorHandler.SwitchTo(ActivityEditorMode.AdditionalEditors);
        }

        public void Layout(Action repaintCallback)
        {
            if (_combinerState.Combiner == null) return;

            var decider = _combinerState.Combiner.GetDecider();

            GUILayout.BeginHorizontal(GUILayout.Width(CgeActivityEditorCombiner.CombinerPreviewWidth * 3));

            GUILayout.BeginVertical(GUILayout.MaxWidth(CgeActivityEditorCombiner.CombinerPreviewWidth));
            GUILayout.Box(_combinerState.Combiner.LeftTexture(), GUILayout.Width(CgeActivityEditorCombiner.CombinerPreviewWidth), GUILayout.Height(CgeActivityEditorCombiner.CombinerPreviewHeight));
            LayoutIntersectionDecider(decider.Intersection, Side.Left);
            LayoutSideDecider(decider.Left, Side.Left);
            GUILayout.Space(CgeLayoutCommon.SingleLineHeight * 2);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.MaxWidth(CgeActivityEditorCombiner.CombinerPreviewCenterWidth), GUILayout.Width(CgeActivityEditorCombiner.CombinerPreviewCenterWidth));
            GUILayout.Space(20);
            GUILayout.Box(_combinerState.Combiner.CombinedTexture(), GUILayout.Width(CgeActivityEditorCombiner.CombinerPreviewCenterWidth), GUILayout.Height(CgeActivityEditorCombiner.CombinerPreviewCenterHeight));
            _combinerState.CombinerCandidateFileName = GUILayout.TextField(_combinerState.CombinerCandidateFileName, GUILayout.MaxWidth(CgeActivityEditorCombiner.CombinerPreviewCenterWidth));
            if (GUILayout.Button("Save and assign to " + _driver.ShortTranslation((_combinerState.CombinerIsAPermutation ? "p_" : "") + _combinerState.CombinerTarget), GUILayout.MaxWidth(CgeActivityEditorCombiner.CombinerPreviewCenterWidth)))
            {
                Save(repaintCallback);
            }
            GUILayout.Space(CgeLayoutCommon.SingleLineHeight * 2);
            _combinerState.ComplexCombiner = EditorGUILayout.Toggle(CgeLocale.CGEE_CombinerShowHidden, _combinerState.ComplexCombiner);
            _combinerState.ShowFullNames = EditorGUILayout.Toggle(CgeLocale.CGEE_CombinerShowFullPaths, _combinerState.ShowFullNames);
            GUILayout.Space(CgeLayoutCommon.SingleLineHeight * 2);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.MaxWidth(CgeActivityEditorCombiner.CombinerPreviewWidth));
            GUILayout.Box(_combinerState.Combiner.RightTexture(), GUILayout.Width(CgeActivityEditorCombiner.CombinerPreviewWidth), GUILayout.Height(CgeActivityEditorCombiner.CombinerPreviewHeight));
            LayoutIntersectionDecider(decider.Intersection, Side.Right);
            LayoutSideDecider(decider.Right, Side.Right);
            GUILayout.Space(CgeLayoutCommon.SingleLineHeight * 2);
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }

        private void Save(Action repaintCallback)
        {
            var savedClip = _combinerState.Combiner.SaveTo(_combinerState.CombinerCandidateFileName);
            _renderingCommands.InvalidateSome(repaintCallback, savedClip);

            _editorHandler.SpProperty(_combinerState.CombinerTarget).objectReferenceValue = savedClip;
            _editorHandler.ApplyModifiedProperties();

            if (_combinerState.CombinerIsLikelyEyesClosed)
            {
                _editorHandler.GetActivity().blinking.Add(savedClip);
            }

            _editorHandler.SwitchTo(ActivityEditorMode.SetFaceExpressions);
        }

        private void LayoutSideDecider(List<SideDecider> sideDeciders, Side side)
        {
            var duplicate = sideDeciders.ToList(); // collection is being modified while iterating and I don't have time to fix this
            foreach (var sideDecider in duplicate)
            {
                var value = sideDecider.Choice;
                GUILayout.BeginHorizontal();

                if (side == Side.Left) {
                    EditorGUI.BeginDisabledGroup(!value);
                    GUILayout.Label(ToFormattedName(sideDecider.Key, value, _combinerState.ShowFullNames), _common.NormalFont);
                    EditorGUI.EndDisabledGroup();
                }

                if (GUILayout.Button((value ? "" : "(") + sideDecider.SampleValue + (value ? "" : ")"), GUILayout.Width(50 - (sideDecider.SampleValue == 0 ? 20 : 0))))
                {
                    _combinerState.Combiner.UpdateSide(side, sideDecider.Key, sideDecider.SampleValue, !value);
                }

                if (side == Side.Right) {
                    EditorGUI.BeginDisabledGroup(!value);
                    GUILayout.Label(ToFormattedName(sideDecider.Key, value, _combinerState.ShowFullNames), _common.NormalFont);
                    EditorGUI.EndDisabledGroup();
                }

                GUILayout.EndHorizontal();
            }
        }

        private void LayoutIntersectionDecider(List<IntersectionDecider> intersectionDeciders, Side side)
        {
            var duplicate = intersectionDeciders.ToList(); // collection is being modified while iterating and I don't have time to fix this
            foreach (var intersectionDecider in duplicate)
            {
                var currentChoice = intersectionDecider.Choice;
                var valueAsBool = side == Side.Left && currentChoice == IntersectionChoice.UseLeft ||
                        side == Side.Right && currentChoice == IntersectionChoice.UseRight;

                var formattedName = ToFormattedName(
                    intersectionDecider.Key,
                    valueAsBool,
                    _combinerState.ShowFullNames
                );

                if (_combinerState.ComplexCombiner || (side == Side.Left && intersectionDecider.SampleLeftValue != 0 || side == Side.Right && intersectionDecider.SampleRightValue != 0))
                {
                    GUILayout.BeginHorizontal();
                    if (side == Side.Left)
                    {
                        EditorGUI.BeginDisabledGroup(!valueAsBool);
                        GUILayout.Label(formattedName, _common.NormalFont);
                        EditorGUI.EndDisabledGroup();
                    }
                    var sampleValue = side == Side.Left ? intersectionDecider.SampleLeftValue : intersectionDecider.SampleRightValue;
                    bool useButton;
                    if (intersectionDecider.SampleLeftValue != intersectionDecider.SampleRightValue)
                    {
                        string message;
                        if (intersectionDecider.Choice != IntersectionChoice.UseNone)
                        {
                            message = (side == Side.Left && valueAsBool ? "← " : "") + sampleValue + (side == Side.Right && valueAsBool ? " →" : "");
                        }
                        else
                        {
                            message = "(" + sampleValue + ")";
                        }
                        useButton = GUILayout.Button(message, GUILayout.Width(80 - (sampleValue == 0 ? 50 : 0)));
                    }
                    else
                    {
                        string message;
                        if (intersectionDecider.Choice != IntersectionChoice.UseNone)
                        {
                            message = "" + sampleValue;
                        }
                        else
                        {
                            message = "(" + sampleValue + ")";
                        }
                        useButton = GUILayout.Button(message, GUILayout.Width(50 - (intersectionDecider.SampleLeftValue == 0 ? 20 : 0)));
                    }
                    if (useButton)
                    {
                        IntersectionChoice newChoice;
                        if (_combinerState.ComplexCombiner || intersectionDecider.SampleLeftValue != 0 && intersectionDecider.SampleRightValue != 0)
                        {
                            if (side == Side.Left)
                            {
                                newChoice = currentChoice != IntersectionChoice.UseLeft ? IntersectionChoice.UseLeft : IntersectionChoice.UseNone;
                            }
                            else
                            {
                                newChoice = currentChoice != IntersectionChoice.UseRight ? IntersectionChoice.UseRight : IntersectionChoice.UseNone;
                            }
                        }
                        else
                        {
                            if (side == Side.Left)
                            {
                                newChoice = currentChoice != IntersectionChoice.UseLeft ? IntersectionChoice.UseLeft : IntersectionChoice.UseRight;
                            }
                            else
                            {
                                newChoice = currentChoice != IntersectionChoice.UseRight ? IntersectionChoice.UseRight : IntersectionChoice.UseLeft;
                            }
                        }
                        _combinerState.Combiner.UpdateIntersection(intersectionDecider, newChoice);
                    }
                    if (side == Side.Right) {
                        EditorGUI.BeginDisabledGroup(!valueAsBool);
                        GUILayout.Label(formattedName, _common.NormalFont);
                        EditorGUI.EndDisabledGroup();
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }

        private static string ToFormattedName(CgeCurveKey key, bool value, bool showFullPath)
        {
            var propertyName =
                key.PropertyName
                    .Replace("blendShape.", "::")
                    .Replace("localEulerAnglesRaw.", "rotation:")
                    .Replace("m_LocalScale.", "scale:")
                    .Replace("m_LocalPosition.", "position:");
            var keyPath = showFullPath ? key.Path : SimplifyPath(key.Path);
            var niceName = propertyName + " @ " + keyPath;
            if (value)
            {
                return "<b>" + niceName + "</b>";
            }

            return niceName;
        }

        private static string SimplifyPath(string keyPath)
        {
            var split = keyPath.Split('/');
            var parts = split.Length - 1;
            if (parts <= 3)
            {
                return keyPath;
            }

            return "../" + split[split.Length - 2] + "/" + split[split.Length - 1];
        }
    }
}
