﻿using System;
using System.IO;
using Hai.ComboGesture.Scripts.Components;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Hai.ComboGesture.Scripts.Editor.EditorUI.Layouts
{
    internal class CgeLayoutSetFaceExpressions
    {
        private readonly CgeLayoutCommon _common;
        private readonly CgeActivityEditorDriver _driver;
        private readonly CgeLayoutFaceExpressionCombiner _layoutFaceExpressionCombiner; // FIXME: It is not normal to have the layout combiner here
        private readonly CgeEditorHandler _editorHandler;
        private readonly Action _repaintCallback;
        private readonly CgeBlendTreeHandler _blendTreeHandler;

        public CgeLayoutSetFaceExpressions(CgeLayoutCommon common, CgeActivityEditorDriver driver, CgeLayoutFaceExpressionCombiner layoutFaceExpressionCombiner, CgeEditorHandler editorHandler, Action repaintCallback, CgeBlendTreeHandler blendTreeHandler)
        {
            _common = common;
            _driver = driver;
            _layoutFaceExpressionCombiner = layoutFaceExpressionCombiner;
            _editorHandler = editorHandler;
            _repaintCallback = repaintCallback;
            _blendTreeHandler = blendTreeHandler;
        }

        public void Layout(Rect position)
        {
            var mode = _editorHandler.GetActivity().activityMode;
            if (mode == ComboGestureActivity.CgeActivityMode.LeftHandOnly || mode == ComboGestureActivity.CgeActivityMode.RightHandOnly)
            {
                LayoutOneHandEditor(position);
            }
            else if (_editorHandler.GetActivity().activityMode == ComboGestureActivity.CgeActivityMode.Permutations)
            {
                LayoutPermutationEditor(position);
            }
            else
            {
                LayoutActivityEditor(position);
            }
        }

        private void LayoutOneHandEditor(Rect position)
        {
            BeginLayoutUsing(CgeLayoutCommon.GuiSquareHeight * 4, position);
            LayoutOneHandMode();
            CgeLayoutCommon.EndLayout();
        }

        private void LayoutOneHandMode()
        {
            LayoutAllOneHandModePage();

            for (var side = 0; side < 8; side++)
            {
                // if (side == 1) continue;

                GUILayout.BeginArea(RectAt(side, 1));
                DrawInner("anim0" + side);
                GUILayout.EndArea();
            }

            GUILayout.BeginArea(RectAt(0, 2));
            DrawTransitionEdit();
            GUILayout.EndArea();
        }

        private void LayoutActivityEditor(Rect position)
        {
            BeginLayoutUsing(CgeLayoutCommon.GuiSquareHeight * 8, position);
            LayoutFullMatrixProjection();
            CgeLayoutCommon.EndLayout();
        }

        private void LayoutAllPermutationTogglePage()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(800));
            GUILayout.Label("<b>" + CgeLocale.CGEE_Permutations + "</b>", _common.LargeFont);
            GUILayout.Label(CgeLocale.CGEE_PermutationsIntro);

            if (GUILayout.Button(new GUIContent(CgeLocale.CGEE_Open_Documentation_and_tutorials, CgeLayoutCommon.GuideIcon32)))
            {
                Application.OpenURL(CgeLocale.PermutationsDocumentationUrl());
            }

            GUILayout.Space(15);
            GUILayout.Label(CgeLocale.CGEE_ConfirmUsePermutations, _common.LargeFont);
            var prev = _editorHandler.SpEnablePermutations().boolValue;
            var current = GUILayout.Toggle(prev, CgeLocale.CGEE_Enable_permutations_for_this_Activity, GUILayout.Width(300));
            if (current != prev)
            {
                if (current)
                {
                    _editorHandler.SpEnablePermutations().boolValue = true;
                    _editorHandler.SpEditorTool().intValue = 2;
                }
                else
                {
                    _editorHandler.SpEnablePermutations().boolValue = false;
                    _editorHandler.SpEditorTool().intValue = 1;
                }
            }

            GUILayout.Label(CgeLocale.CGEE_PermutationsFootnote);

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void LayoutAllOneHandModePage()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(800));
            GUILayout.Label("<b>" + CgeLocale.CGEE_OneHandMode + "</b>", _common.LargeFont);
            GUILayout.Label(CgeLocale.CGEE_OneHandModeIntro);

            GUILayout.Space(15);
            DrawOneHandModeSwitcher();

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DrawOneHandModeSwitcher()
        {
            var previousValue = _editorHandler.SpOneHandMode().intValue;
            EditorGUILayout.PropertyField(_editorHandler.SpOneHandMode(), GUILayout.Width(800));
            var newValue = _editorHandler.SpOneHandMode().intValue;
            if (previousValue != newValue)
            {
                if (previousValue == 0 && newValue != 0)
                {
                    _editorHandler.SpEditorTool().intValue = 0;
                }

                if (previousValue != 0 && newValue == 0)
                {
                    _editorHandler.SpEditorTool().intValue = 5;
                }
            }
        }

        private void LayoutPermutationEditor(Rect position)
        {
            switch (_editorHandler.SpEditorTool().intValue)
            {
                case 0:
                    BeginPermutationLayoutUsing(position);
                    LayoutPermutationMatrixProjection(true);
                    CgeLayoutCommon.EndLayout();
                    break;
                case 1:
                default:
                    BeginPermutationLayoutUsing(position);
                    LayoutPermutationMatrixProjection();
                    CgeLayoutCommon.EndLayout();
                    break;
            }
        }

        private void BeginLayoutUsing(int totalHeight, Rect position)
        {
            var totalWidth = CgeLayoutCommon.GuiSquareWidth * 8;
            GUILayout.Box("", GUIStyle.none, GUILayout.Width(totalWidth), GUILayout.Height(totalHeight));
            GUILayout.BeginArea(new Rect(Math.Max((position.width - totalWidth) / 2, 0), 0, totalWidth, totalHeight));
        }

        private void BeginPermutationLayoutUsing(Rect position)
        {
            var totalHeight = CgeLayoutCommon.GuiSquareHeight * 9;
            var totalWidth = CgeLayoutCommon.GuiSquareWidth * 9;
            GUILayout.Box("", GUIStyle.none, GUILayout.Width(totalWidth), GUILayout.Height(totalHeight));
            GUILayout.BeginArea(new Rect(Math.Max((position.width - totalWidth) / 2, 0), 0, totalWidth, totalHeight));
        }

        private void LayoutFistMatrixProjection()
        {
            for (var side = 0; side < 8; side++)
            {
                if (side == 1) continue;

                GUILayout.BeginArea(RectAt(side, 0));
                DrawInner("anim0" + side);
                GUILayout.EndArea();

                GUILayout.BeginArea(RectAt(side, 1));
                if (side == 0)
                {
                    DrawInner("anim01");
                }
                else
                {
                    DrawInner("anim1" + side);
                }
                GUILayout.EndArea();
            }

            GUILayout.BeginArea(RectAt(1, 0));
            DrawTransitionEdit();
            GUILayout.EndArea();

            GUILayout.BeginArea(RectAt(0, 3));
            DrawInner("anim00");
            GUILayout.EndArea();
            GUILayout.BeginArea(RectAt(1, 3));
            DrawInner("anim00");
            GUILayout.EndArea();
            GUILayout.BeginArea(RectAt(2, 3));
            DrawInner("anim00");
            GUILayout.EndArea();
            GUILayout.BeginArea(RectAt(0, 4));
            DrawInner("anim11_L");
            GUILayout.EndArea();
            GUILayout.BeginArea(RectAt(1, 4));
            DrawInner("anim11");
            GUILayout.EndArea();
            GUILayout.BeginArea(RectAt(2, 4));
            DrawInner("anim11_R");
            GUILayout.EndArea();
        }

        private void LayoutSinglesDoublesMatrixProjection()
        {
            for (var side = 0; side < 8; side++)
            {
                if (side == 1) continue;

                GUILayout.BeginArea(RectAt(side, 0));
                DrawInner("anim0" + side);
                GUILayout.EndArea();

                if (side != 0) {
                    GUILayout.BeginArea(RectAt(side, 1));
                    DrawInner("anim" + side + "" + side);
                    GUILayout.EndArea();
                }
            }

            GUILayout.BeginArea(RectAt(1, 0));
            DrawTransitionEdit();
            GUILayout.EndArea();
        }

        private void LayoutComboMatrixProjection()
        {
            for (var sideA = 0; sideA < 8; sideA++)
            {
                for (var sideB = 0; sideB < 8; sideB++)
                {
                    if (sideA == 0 && sideB == 0 || sideA != sideB && sideA != 1 && sideB != 1)
                    {
                        int left, right;
                        if (sideA <= sideB)
                        {
                            left = sideA;
                            right = sideB;
                        }
                        else
                        {
                            left = sideB;
                            right = sideA;
                        }
                        GUILayout.BeginArea(RectAt(sideB, sideA));
                        DrawInner("anim" + left + "" + right);
                        GUILayout.EndArea();
                    }
                }
            }

            GUILayout.BeginArea(RectAt(1, 0));
            DrawTransitionEdit();
            GUILayout.EndArea();
        }

        private void LayoutPermutationMatrixProjection(bool partial = false)
        {
            for (var sideA = 0; sideA < 8; sideA++)
            {
                for (var sideB = 0; sideB < 8; sideB++)
                {
                    GUILayout.BeginArea(RectAt(sideB, sideA));
                    DrawInner("anim" + sideA + "" + sideB, "anim" + sideB + "" + sideA, partial);
                    GUILayout.EndArea();
                }
            }
            for (var side = 1; side < 8; side++)
            {
                GUILayout.BeginArea(RectAt(8, side));
                DrawInner("anim0" + side, "anim" + side + "0", partial);
                GUILayout.EndArea();

                GUILayout.BeginArea(RectAt(side, 8));
                DrawInner("anim" + side + "0", "anim0" + side, partial);
                GUILayout.EndArea();
            }

            GUILayout.BeginArea(RectAt(0, 8));
            CgeLayoutCommon.DrawColoredBackground(CgeLayoutCommon.LeftSideBg);
            DrawInner("anim11_L");
            GUILayout.EndArea();
            GUILayout.BeginArea(RectAt(8, 0));
            CgeLayoutCommon.DrawColoredBackground(CgeLayoutCommon.RightSideBg);
            DrawInner("anim11_R");
            GUILayout.EndArea();

            GUILayout.BeginArea(RectAt(8, 8));
            DrawTransitionEdit();

            GUILayout.EndArea();
        }

        private void LayoutFullMatrixProjection()
        {
            for (var left = 0; left < 8; left++)
            {
                for (var right = left; right < 8; right++)
                {
                    GUILayout.BeginArea(RectAt(right, left));
                    DrawInner("anim" + left + "" + right);
                    GUILayout.EndArea();
                }
            }

            GUILayout.BeginArea(RectAt(0, 1));
            DrawTransitionEdit();
            GUILayout.EndArea();

            GUILayout.BeginArea(RectAt(0, 4));
            DrawInner("anim11_L");
            GUILayout.EndArea();

            GUILayout.BeginArea(RectAt(2, 4));
            DrawInner("anim11_R");
            GUILayout.EndArea();
        }

        private static Rect RectAt(int xGrid, int yGrid)
        {
            return new Rect(xGrid * CgeLayoutCommon.GuiSquareWidth, yGrid * CgeLayoutCommon.GuiSquareHeight, CgeLayoutCommon.GuiSquareWidth - 3, CgeLayoutCommon.GuiSquareHeight - 3);
        }

        private void DrawInner(string propertyPath, string oppositePath = null, bool partial = false)
        {
            var usePermutations = oppositePath != null;
            var property = _editorHandler.SpProperty(propertyPath);
            var oppositeProperty = usePermutations ? _editorHandler.SpProperty(oppositePath) : null;
            var isLeftHand = String.Compare(propertyPath, oppositePath, StringComparison.Ordinal) > 0;
            if (usePermutations)
            {
                if (propertyPath == oppositePath)
                {
                    CgeLayoutCommon.DrawColoredBackground(CgeLayoutCommon.NeutralSideBg);
                }
                else if (property.objectReferenceValue == null && oppositeProperty.objectReferenceValue == null
                         || isLeftHand && property.objectReferenceValue == null && oppositeProperty.objectReferenceValue != null
                         || !isLeftHand && property.objectReferenceValue != null && oppositeProperty.objectReferenceValue == null)
                {
                    if (isLeftHand && partial)
                    {
                        return;
                    }

                    CgeLayoutCommon.DrawColoredBackground(isLeftHand ? CgeLayoutCommon.LeftSymmetricalBg : CgeLayoutCommon.RightSymmetricalBg);
                }
                else if (oppositeProperty.objectReferenceValue == property.objectReferenceValue || isLeftHand && oppositeProperty.objectReferenceValue == null || !isLeftHand && property.objectReferenceValue == null)
                {
                    CgeLayoutCommon.DrawColoredBackground(CgeLayoutCommon.InconsistentBg);
                }
                else
                {
                    CgeLayoutCommon.DrawColoredBackground(isLeftHand ? CgeLayoutCommon.LeftSideBg : CgeLayoutCommon.RightSideBg);
                }
            }

            var translatableProperty = usePermutations
                ? (partial && !isLeftHand && oppositeProperty.objectReferenceValue == null ? propertyPath : ("p_" + propertyPath))
                : propertyPath;
            var isSymmetrical = _driver.IsSymmetrical(translatableProperty);
            if (!usePermutations)
            {
                CgeLayoutCommon.DrawColoredBackground(CgeLayoutCommon.NeutralSideBg);
            }

            GUILayout.Label(_driver.ShortTranslation(translatableProperty), isSymmetrical ? _common.MiddleAlignedBold : _common.MiddleAligned);

            var element = property.objectReferenceValue != null ? (Motion) property.objectReferenceValue : null;
            if (element != null)
            {
                GUILayout.BeginArea(new Rect((CgeLayoutCommon.GuiSquareWidth - CgeLayoutCommon.PictureWidth) / 2, CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.PictureWidth, CgeLayoutCommon.PictureHeight));
                _common.DrawPreviewOrRefreshButton(element);
                GUILayout.EndArea();
            }
            else if (usePermutations)
            {
                if (oppositeProperty.objectReferenceValue != null && propertyPath != oppositePath && isLeftHand)
                {
                    DrawInnerReversal(oppositePath);
                }
            }
            else
            {
                // ReSharper disable once PossibleLossOfFraction
                GUILayout.BeginArea(new Rect((CgeLayoutCommon.GuiSquareWidth - CgeLayoutCommon.PictureWidth) / 2, CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.PictureWidth, CgeLayoutCommon.PictureHeight));
                GUILayout.EndArea();
            }


            if (_driver.IsAPropertyThatCanBeCombined(propertyPath, usePermutations) && !(element is BlendTree))
            {
                var rect = element is AnimationClip
                    ? new Rect(-3 + CgeLayoutCommon.GuiSquareWidth - 2 * CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 0.5f, CgeLayoutCommon.SingleLineHeight * 2, CgeLayoutCommon.SingleLineHeight * 1.5f)
                    : new Rect(-3 + CgeLayoutCommon.GuiSquareWidth - 100, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 0.5f, 100, CgeLayoutCommon.SingleLineHeight * 1.5f);

                var areSourcesCompatible = _driver.AreCombinationSourcesDefinedAndCompatible(propertyPath, usePermutations);
                EditorGUI.BeginDisabledGroup(!areSourcesCompatible);
                GUILayout.BeginArea(rect);
                if (ColoredBackground(usePermutations, isLeftHand ? CgeLayoutCommon.LeftSideBg : CgeLayoutCommon.RightSideBg, () => GUILayout.Button((element != null ? "+" : "+ " + CgeLocale.CGEE_Combine))))
                {
                    var merge = _driver.ProvideCombinationPropertySources(propertyPath, usePermutations);
                    OpenMergeWindowFor(merge.Left, merge.Right, propertyPath, usePermutations);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else if (element is BlendTree)
            {
                var rect = new Rect(-3 + CgeLayoutCommon.GuiSquareWidth - 20, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 0.5f, 20, CgeLayoutCommon.SingleLineHeight * 1.5f);

                EditorGUI.BeginDisabledGroup(false);
                GUILayout.BeginArea(rect);
                if (GUILayout.Button("?"))
                {
                    OpenBlendTreeAt(propertyPath);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                BeginInvisibleRankPreservingArea();
                CgeLayoutCommon.InvisibleRankPreservingButton();
                EndInvisibleRankPreservingArea();
            }

            if (_driver.IsAPropertyThatCanBeCombinedDiagonally(propertyPath, usePermutations) && !(element is BlendTree))
            {
                var rect = !_driver.IsSymmetrical(propertyPath) || element != null
                    ? new Rect(0, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 0.5f, CgeLayoutCommon.SingleLineHeight * 2, CgeLayoutCommon.SingleLineHeight * 1.5f)
                    : new Rect(0, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 0.5f, 120, CgeLayoutCommon.SingleLineHeight * 1.5f);

                var areSourcesCompatible = _driver.AreDiagonalCombinationSourcesDefinedAndCompatible(propertyPath, usePermutations);
                EditorGUI.BeginDisabledGroup(!areSourcesCompatible);
                GUILayout.BeginArea(rect);
                if (ColoredBackground(!isSymmetrical, !isLeftHand ? CgeLayoutCommon.LeftSideBg : CgeLayoutCommon.RightSideBg, () => GUILayout.Button(!_driver.IsSymmetrical(propertyPath) || element != null ? "⅃" : "⅃ " + CgeLocale.CGEE_CombineAcross)))
                {
                    var merge = _driver.ProvideDiagonalCombinationPropertySources(propertyPath, usePermutations);
                    OpenMergeWindowFor(merge.Left, merge.Right, propertyPath, usePermutations);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }

            if (element == null && !_driver.IsAPropertyThatCanBeCombined(propertyPath, usePermutations))
            {
                EditorGUI.BeginDisabledGroup(false);
                GUILayout.BeginArea(new Rect(-3 + 10, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 3f, CgeLayoutCommon.GuiSquareWidth - 20, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button("❈ " + CgeLocale.CGEE_Create))
                {
                    CreateNewAnimation(propertyPath);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else if (element != null && element is AnimationClip clip)
            {
                GUILayout.BeginArea(new Rect(CgeLayoutCommon.GuiSquareWidth - 20, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 3f, 20, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button("❈"))
                {
                    EditAnimation(clip);
                }
                GUILayout.EndArea();
            }

            if (usePermutations && propertyPath != oppositePath && property.objectReferenceValue == oppositeProperty.objectReferenceValue && property.objectReferenceValue != null)
            {
                EditorGUI.BeginDisabledGroup(false);
                GUILayout.BeginArea(new Rect(-3 + 10, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 1.75f, CgeLayoutCommon.GuiSquareWidth - 10, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button("↗↗ " + CgeLocale.CGEE_Simplify))
                {
                    Simplify(isLeftHand ? propertyPath : oppositePath);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else if (usePermutations && (isLeftHand && property.objectReferenceValue != null && oppositeProperty.objectReferenceValue == null || !isLeftHand && property.objectReferenceValue == null && oppositeProperty.objectReferenceValue != null))
            {
                EditorGUI.BeginDisabledGroup(false);
                GUILayout.BeginArea(new Rect(-3 + 10, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 1.75f, CgeLayoutCommon.GuiSquareWidth - 10, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button("↗↙ " + CgeLocale.CGEE_SwapToFix))
                {
                    Swap(propertyPath, oppositePath);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else if (usePermutations && isLeftHand && property.objectReferenceValue != null)
            {
                EditorGUI.BeginDisabledGroup(false);
                GUILayout.BeginArea(new Rect(-3 + CgeLayoutCommon.GuiSquareWidth - 2 * CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 1.75f, 2 * CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button("↗↙"))
                {
                    Swap(propertyPath, oppositePath);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else if (element == null && _driver.IsAutoSettable(propertyPath))
            {
                var propertyPathToCopyFrom = _driver.GetAutoSettableSource(propertyPath);
                var animationToBeCopied = _editorHandler.SpProperty(propertyPathToCopyFrom).objectReferenceValue;

                EditorGUI.BeginDisabledGroup(animationToBeCopied == null);
                GUILayout.BeginArea(new Rect(-3 + CgeLayoutCommon.GuiSquareWidth - 100, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 1.75f, 100, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button(CgeLocale.CGEE_AutoSet))
                {
                    AutoSet(propertyPath, propertyPathToCopyFrom);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else if (element == null && _driver.AreCombinationSourcesIdentical(propertyPath))
            {
                var propertyPathToCopyFrom = _driver.ProvideCombinationPropertySources(propertyPath, usePermutations).Left;
                var animationToBeCopied = _editorHandler.SpProperty(propertyPathToCopyFrom).objectReferenceValue;

                EditorGUI.BeginDisabledGroup(animationToBeCopied == null);
                GUILayout.BeginArea(new Rect(-3 + CgeLayoutCommon.GuiSquareWidth - 100, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight * 1.75f, 100, CgeLayoutCommon.SingleLineHeight * 1.5f));
                if (GUILayout.Button(CgeLocale.CGEE_AutoSet))
                {
                    AutoSet(propertyPath, propertyPathToCopyFrom);
                }
                GUILayout.EndArea();
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                BeginInvisibleRankPreservingArea();
                CgeLayoutCommon.InvisibleRankPreservingButton();
                EndInvisibleRankPreservingArea();
            }

            GUILayout.Space(CgeLayoutCommon.PictureHeight);
            EditorGUILayout.PropertyField(property, GUIContent.none);
        }

        private void EditAnimation(AnimationClip clip)
        {
            CgeEditorWindow.ShowExpressionsEditor(_editorHandler, clip);
            EditorGUIUtility.PingObject(clip);
            Selection.SetActiveObjectWithContext(clip, null);
        }

        private void CreateNewAnimation(string propertyPath)
        {
            var animations = _editorHandler.AllDistinctAnimations();
            var folder = animations.Count > 0 ? AssetDatabase.GetAssetPath(animations[0]).Replace(Path.GetFileName(AssetDatabase.GetAssetPath(animations[0])), "") : "Assets/";

            var finalFilename = "CGE_NewAnimation__" + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HHmmss") + ".anim";

            var finalPath = folder + finalFilename;
            var clip = new AnimationClip();
            AssetDatabase.CreateAsset(clip, finalPath);
            AssetDatabase.LoadAssetAtPath<AnimationClip>(finalPath);

            _editorHandler.SpProperty(propertyPath).objectReferenceValue = clip;
            _editorHandler.ApplyModifiedProperties();

            CgeEditorWindow.ShowExpressionsEditor(_editorHandler, clip);
            EditorGUIUtility.PingObject(clip);
            Selection.SetActiveObjectWithContext(clip, null);
        }

        private void DrawInnerReversal(string propertyPath)
        {
            var property = _editorHandler.SpProperty(propertyPath);

            var edge = CgeLayoutCommon.GuiSquareWidth - CgeLayoutCommon.PictureWidth;
            GUILayout.BeginArea(new Rect(edge, CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.PictureWidth - edge, CgeLayoutCommon.PictureHeight - CgeLayoutCommon.SingleLineHeight));
            var element = (Motion)property.objectReferenceValue;
            _common.DrawPreviewOrRefreshButton(element, true);
            GUILayout.EndArea();
        }

        private void DrawTransitionEdit()
        {
            GUILayout.BeginArea(new Rect((CgeLayoutCommon.GuiSquareWidth - CgeLayoutCommon.PictureWidth) / 2, CgeLayoutCommon.SingleLineHeight, CgeLayoutCommon.PictureWidth, CgeLayoutCommon.PictureHeight));
            GUILayout.Label(CgeLocale.CGEE_Transition_duration_in_seconds);
            EditorGUILayout.Slider(_editorHandler.SpTransitionDuration(), 0f, 1f, GUIContent.none);
            GUILayout.EndArea();
            GUILayout.Space(CgeLayoutCommon.PictureHeight);
        }

        private static void BeginInvisibleRankPreservingArea()
        {
            GUILayout.BeginArea(new Rect(-1000, -1000, 0, 0));
        }

        private static void EndInvisibleRankPreservingArea()
        {
            GUILayout.EndArea();
        }

        private void OpenMergeWindowFor(string left, string right, string propertyPath, bool usePermutations)
        {
            var leftAnim = _editorHandler.SpProperty(left).objectReferenceValue;
            var rightAnim = _editorHandler.SpProperty(right).objectReferenceValue;

            var areBothAnimations = leftAnim is AnimationClip && rightAnim is AnimationClip;
            if (!areBothAnimations) return;

            _layoutFaceExpressionCombiner.DoSetCombiner((AnimationClip) leftAnim, (AnimationClip) rightAnim, propertyPath, usePermutations, _repaintCallback);
        }

        private void OpenBlendTreeAt(string propertyPath)
        {
            var blendTree = (BlendTree)_editorHandler.SpProperty(propertyPath).objectReferenceValue;
            _blendTreeHandler.BlendTreeBeingEdited = blendTree;
            _editorHandler.SwitchAdditionalEditorTo(AdditionalEditorsMode.ViewBlendTrees);
            if (_editorHandler.GetCurrentlyEditing() == CurrentlyEditing.Activity)
            {
                _editorHandler.SwitchTo(ActivityEditorMode.AdditionalEditors);
            }
        }

        private void AutoSet(string propertyPath, string propertyPathToCopyFrom)
        {
            _editorHandler.SpProperty(propertyPath).objectReferenceValue = _editorHandler.SpProperty(propertyPathToCopyFrom).objectReferenceValue;
            _editorHandler.ApplyModifiedProperties();
        }

        private void Swap(string propertyPath, string oppositePath)
        {
            var aProp = _editorHandler.SpProperty(propertyPath);
            var bProp = _editorHandler.SpProperty(oppositePath);
            var a = aProp.objectReferenceValue;
            var b = bProp.objectReferenceValue;

            aProp.objectReferenceValue = b;
            bProp.objectReferenceValue = a;
            _editorHandler.ApplyModifiedProperties();
        }

        private void Simplify(string pathToClear)
        {
            _editorHandler.SpProperty(pathToClear).objectReferenceValue = null;
            _editorHandler.ApplyModifiedProperties();
        }

        private static T ColoredBackground<T>(bool isActive, Color bgColor, Func<T> inside)
        {
            var col = GUI.color;
            try
            {
                if (isActive) GUI.color = bgColor;
                return inside();
            }
            finally
            {
                GUI.color = col;
            }
        }
    }
}
