﻿//Copyright (c) 2007-2012, Adolfo Marinucci
//All rights reserved.

//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
//following conditions are met:

//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following 
//disclaimer in the documentation and/or other materials provided with the distribution.

//* Neither the name of Adolfo Marinucci nor the names of its contributors may be used to endorse or promote products
//derived from this software without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
//STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
//EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using AvalonDock.Layout;

namespace AvalonDock.Controls
{
    internal class DocumentPaneDropAsAnchorableTarget : DropTarget<LayoutDocumentPaneControl>
    {
        internal DocumentPaneDropAsAnchorableTarget(LayoutDocumentPaneControl paneControl, Rect detectionRect, DropTargetType type)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
        }

        internal DocumentPaneDropAsAnchorableTarget(LayoutDocumentPaneControl paneControl, Rect detectionRect, DropTargetType type, int tabIndex)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
            _tabIndex = tabIndex;
        }


        LayoutDocumentPaneControl _targetPane;

        int _tabIndex = -1;

        protected override void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
            ILayoutDocumentPane targetModel = _targetPane.Model as ILayoutDocumentPane;
            LayoutDocumentPaneGroup parentGroup;
            LayoutPanel parentGroupPanel;
            FindParentLayoutDocumentPane(targetModel, out parentGroup, out parentGroupPanel);

            switch (Type)
            {
                case DropTargetType.DocumentPaneDockAsAnchorableBottom:
                    #region DropTargetType.DocumentPaneDockAsAnchorableBottom
                    {

                        //var parentGroup = targetModel.Parent as LayoutDocumentPaneGroup;
                        //var parentGroupPanel = targetModel.FindParent<LayoutPanel>();
                        //if (parentGroup != null)
                        //    parentGroupPanel = parentGroup.Parent as LayoutPanel;
                        //else
                        //    parentGroupPanel = targetModel.Parent as LayoutPanel;

                        if (parentGroupPanel != null &&
                            parentGroupPanel.Orientation == System.Windows.Controls.Orientation.Vertical)
                        {
                            parentGroupPanel.Children.Insert(
                                parentGroupPanel.IndexOfChild(parentGroup != null ? parentGroup : targetModel) + 1,
                                floatingWindow.RootPanel);
                        }
                        else if (parentGroupPanel != null)
                        {
                            var newParentPanel = new LayoutPanel() { Orientation = System.Windows.Controls.Orientation.Vertical };
                            parentGroupPanel.ReplaceChild(parentGroup != null ? parentGroup : targetModel, newParentPanel);
                            newParentPanel.Children.Add(parentGroup != null ? parentGroup : targetModel);
                            newParentPanel.Children.Add(floatingWindow.RootPanel);
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    break;
                    #endregion
                case DropTargetType.DocumentPaneDockAsAnchorableTop:
                    #region DropTargetType.DocumentPaneDockAsAnchorableTop
                    {
                        //var parentGroup = targetModel.Parent as LayoutDocumentPaneGroup;
                        //var parentGroupPanel = targetModel.FindParent<LayoutPanel>();
                        //if (parentGroup != null)
                        //    parentGroupPanel = parentGroup.Parent as LayoutPanel;
                        //else
                        //    parentGroupPanel = targetModel.Parent as LayoutPanel;

                        if (parentGroupPanel != null &&
                            parentGroupPanel.Orientation == System.Windows.Controls.Orientation.Vertical)
                        {
                            parentGroupPanel.Children.Insert(
                                parentGroupPanel.IndexOfChild(parentGroup != null ? parentGroup : targetModel),
                                floatingWindow.RootPanel);
                        }
                        else if (parentGroupPanel != null)
                        {
                            var newParentPanel = new LayoutPanel() { Orientation = System.Windows.Controls.Orientation.Vertical };
                            parentGroupPanel.ReplaceChild(parentGroup != null ? parentGroup : targetModel, newParentPanel);
                            newParentPanel.Children.Add(parentGroup != null ? parentGroup : targetModel);
                            newParentPanel.Children.Insert(0, floatingWindow.RootPanel);
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                    }
                    break;
                    #endregion
                case DropTargetType.DocumentPaneDockAsAnchorableLeft:
                    #region DropTargetType.DocumentPaneDockAsAnchorableLeft
                    {
                        //LayoutPanel parentGroupPanel = null;
                        //var parentGroup = targetModel.Parent as LayoutDocumentPaneGroup;
                        //var parentGroupPanel = targetModel.FindParent<LayoutPanel>();

                        //if (parentGroup != null)
                        //    parentGroupPanel = parentGroup.Parent as LayoutPanel;
                        //else
                        //    parentGroupPanel = targetModel.Parent as LayoutPanel;

                        if (parentGroupPanel != null &&
                            parentGroupPanel.Orientation == System.Windows.Controls.Orientation.Horizontal)
                        {
                            parentGroupPanel.Children.Insert(
                                parentGroupPanel.IndexOfChild(parentGroup != null ? parentGroup : targetModel),
                                floatingWindow.RootPanel);
                        }
                        else if (parentGroupPanel != null)
                        {
                            var newParentPanel = new LayoutPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
                            parentGroupPanel.ReplaceChild(parentGroup != null ? parentGroup : targetModel, newParentPanel);
                            newParentPanel.Children.Add(parentGroup != null ? parentGroup : targetModel);
                            newParentPanel.Children.Insert(0, floatingWindow.RootPanel);
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    break;
                    #endregion
                case DropTargetType.DocumentPaneDockAsAnchorableRight:
                    #region DropTargetType.DocumentPaneDockAsAnchorableRight
                    {
                        //var parentGroup = targetModel.Parent as LayoutDocumentPaneGroup;
                        //var parentGroupPanel = targetModel.FindParent<LayoutPanel>();
                        //if (parentGroup != null)
                        //    parentGroupPanel = parentGroup.Parent as LayoutPanel;
                        //else
                        //    parentGroupPanel = targetModel.Parent as LayoutPanel;

                        if (parentGroupPanel != null &&
                            parentGroupPanel.Orientation == System.Windows.Controls.Orientation.Horizontal)
                        {
                            parentGroupPanel.Children.Insert(
                                parentGroupPanel.IndexOfChild(parentGroup != null ? parentGroup : targetModel) + 1,
                                floatingWindow.RootPanel);
                        }
                        else if (parentGroupPanel != null)
                        {
                            var newParentPanel = new LayoutPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
                            parentGroupPanel.ReplaceChild(parentGroup != null ? parentGroup : targetModel, newParentPanel);
                            newParentPanel.Children.Add(parentGroup != null ? parentGroup : targetModel);
                            newParentPanel.Children.Add(floatingWindow.RootPanel);
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    break;
                    #endregion
            }

            base.Drop(floatingWindow);
        }

        public override System.Windows.Media.Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindowModel)
        {
            Rect targetScreenRect;
            ILayoutDocumentPane targetModel = _targetPane.Model as ILayoutDocumentPane;
            var manager = targetModel.Root.Manager;

            //ILayoutDocumentPane targetModel = _targetPane.Model as ILayoutDocumentPane;
            LayoutDocumentPaneGroup parentGroup;
            LayoutPanel parentGroupPanel;
            FindParentLayoutDocumentPane(targetModel, out parentGroup, out parentGroupPanel);
            
            //if (targetModel.Parent is LayoutDocumentPaneGroup)
            //{
            //    var parentGroup = targetModel.Parent as LayoutDocumentPaneGroup;
            //    var documentPaneGroupControl = manager.FindLogicalChildren<LayoutDocumentPaneGroupControl>().First(d => d.Model == parentGroup);
            //    targetScreenRect = documentPaneGroupControl.GetScreenArea();
            //}
            //else
            //{
            //    var documentPaneControl = manager.FindLogicalChildren<LayoutDocumentPaneControl>().First(d => d.Model == targetModel);
            //    targetScreenRect = documentPaneControl.GetScreenArea();
            //}

            //var parentPanel = targetModel.FindParent<LayoutPanel>();
            var documentPaneControl = manager.FindLogicalChildren<FrameworkElement>().OfType<ILayoutControl>().First(d => d.Model == parentGroup) as FrameworkElement;
            targetScreenRect = documentPaneControl.GetScreenArea();

            switch (Type)
            {
                case DropTargetType.DocumentPaneDockAsAnchorableBottom:
                    {
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);
                        targetScreenRect.Offset(0.0, targetScreenRect.Height - targetScreenRect.Height / 3.0);
                        targetScreenRect.Height /= 3.0;
                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.DocumentPaneDockAsAnchorableTop:
                    {
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);
                        targetScreenRect.Height /= 3.0;
                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.DocumentPaneDockAsAnchorableRight:
                    {
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);
                        targetScreenRect.Offset(targetScreenRect.Width - targetScreenRect.Width / 3.0, 0.0);
                        targetScreenRect.Width /= 3.0;
                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.DocumentPaneDockAsAnchorableLeft:
                    {
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);
                        targetScreenRect.Width /= 3.0;
                        return new RectangleGeometry(targetScreenRect);
                    }
            }

            return null;
        }



        bool FindParentLayoutDocumentPane(ILayoutDocumentPane documentPane, out LayoutDocumentPaneGroup containerPaneGroup, out LayoutPanel containerPanel)
        {
            containerPaneGroup = null;
            containerPanel = null;

            if (documentPane.Parent is LayoutPanel)
            {
                containerPaneGroup = null;
                containerPanel = documentPane.Parent as LayoutPanel;
                return true;
            }
            else if (documentPane.Parent is LayoutDocumentPaneGroup)
            {
                var currentDocumentPaneGroup = documentPane.Parent as LayoutDocumentPaneGroup;
                while (!(currentDocumentPaneGroup.Parent is LayoutPanel))
                {
                    currentDocumentPaneGroup = currentDocumentPaneGroup.Parent as LayoutDocumentPaneGroup;

                    if (currentDocumentPaneGroup == null)
                        break;
                }

                if (currentDocumentPaneGroup == null)
                    return false;

                containerPaneGroup = currentDocumentPaneGroup;
                containerPanel = currentDocumentPaneGroup.Parent as LayoutPanel;
                return true;
            }

            return false;


        
        }

    }
}
