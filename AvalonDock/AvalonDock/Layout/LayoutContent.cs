﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Xml.Serialization;
using System.Windows;

namespace AvalonDock.Layout
{
    [ContentProperty("Content")]
    [Serializable]
    public abstract class LayoutContent : LayoutElement, IXmlSerializable
    {
        internal LayoutContent()
        { }

        #region Title

        private string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    RaisePropertyChanging("Title");
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        #endregion

        #region Content
        [NonSerialized]
        private object _content = null;
        [XmlIgnore]
        public object Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    RaisePropertyChanging("Content");
                    _content = value;
                    RaisePropertyChanged("Content");
                }
            }
        }

        #endregion

        #region ContentId

        private string _contentId = null;
        public string ContentId
        {
            get 
            {
                if (_contentId == null)
                { 
                    var contentAsControl = _content as FrameworkElement;
                    if (contentAsControl != null && !string.IsNullOrWhiteSpace(contentAsControl.Name))
                        return contentAsControl.Name;
                }
                return _contentId; 
            }
            set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    RaisePropertyChanged("ContentId");
                }
            }
        }

        #endregion

        #region IsSelected

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    bool oldValue = _isSelected;
                    RaisePropertyChanging("IsSelected");
                    _isSelected = value;
                    var parentSelector = (Parent as ILayoutContentSelector);
                    if (parentSelector != null)
                        parentSelector.SelectedContentIndex = _isSelected ? parentSelector.IndexOf(this) : -1;
                    OnIsSelectedChanged(oldValue, value);
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the IsSelected property.
        /// </summary>
        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
        }

        #endregion

        #region IsActive

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    RaisePropertyChanging("IsActive");
                    bool oldValue = _isActive;
                    _isActive = value;

                    var root = Root;
                    if (root != null && _isActive)
                        root.ActiveContent = this;

                    OnIsActiveChanged(oldValue, value);
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the IsActive property.
        /// </summary>
        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
        }

        #endregion

        #region IsLastFocusedDocument

        private bool _lastFocusedDocument = false;
        public bool IsLastFocusedDocument
        {
            get { return _lastFocusedDocument; }
            set
            {
                if (_lastFocusedDocument != value)
                {
                    RaisePropertyChanging("IsLastFocusedDocument");
                    _lastFocusedDocument = value;
                    RaisePropertyChanged("IsLastFocusedDocument");
                }
            }
        }

        #endregion

        #region PreviousContainer

        [field: NonSerialized]
        private ILayoutPane _previousContainer = null;

        [XmlIgnore]
        public ILayoutPane PreviousContainer
        {
            get { return _previousContainer; }
            internal set
            {
                if (_previousContainer != value)
                {
                    _previousContainer = value;
                    RaisePropertyChanged("PreviousContainer");

                    var paneSerializable = _previousContainer as ILayoutPaneSerializable;
                    if (paneSerializable != null &&
                        paneSerializable.Id == null)
                        paneSerializable.Id = Guid.NewGuid().ToString();
                }
            }
        }

        internal string PreviousContainerId
        {
            get;
            private set;
        }

        #endregion

        #region PreviousContainerIndex
        [field: NonSerialized]
        private int _previousContainerIndex = -1;
        [XmlIgnore]
        public int PreviousContainerIndex
        {
            get { return _previousContainerIndex; }
            set
            {
                if (_previousContainerIndex != value)
                {
                    _previousContainerIndex = value;
                    RaisePropertyChanged("PreviousContainerIndex");
                }
            }
        }

        #endregion

        protected override void OnParentChanging(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            var root = Root;
            if (root != null && _isActive && newValue == null)
                root.ActiveContent = null;

            if (IsSelected && newValue == null && Parent is ILayoutContentSelector)
            {
                var parentSelector = (Parent as ILayoutContentSelector);
                if (parentSelector.SelectedContentIndex == oldValue.ChildrenCount)
                    parentSelector.SelectedContentIndex--;
                
            }
            
            base.OnParentChanging(oldValue, newValue);
        }

        protected override void OnParentChanged(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            if (IsSelected && Parent != null && Parent is ILayoutContentSelector)
            {
                var parentSelector = (Parent as ILayoutContentSelector);
                parentSelector.SelectedContentIndex = parentSelector.IndexOf(this);
            }

            var root = Root;
            if (root != null && _isActive)
                root.ActiveContent = this;

            base.OnParentChanged(oldValue, newValue);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute("Title"))
                Title = reader.Value;
            if (reader.MoveToAttribute("IsSelected"))
                IsSelected = bool.Parse(reader.Value);
            if (reader.MoveToAttribute("IsActive"))
                IsActive = bool.Parse(reader.Value);
            if (reader.MoveToAttribute("ContentId"))
                ContentId = reader.Value;
            if (reader.MoveToAttribute("IsLastFocusedDocument"))
                IsLastFocusedDocument = bool.Parse(reader.Value);
            if (reader.MoveToAttribute("PreviousContainerId"))
                PreviousContainerId = reader.Value;
            if (reader.MoveToAttribute("PreviousContainerIndex"))
                PreviousContainerIndex = int.Parse(reader.Value);

            reader.Read();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(Title))
                writer.WriteAttributeString("Title", Title);
            
            if (IsSelected)
                writer.WriteAttributeString("IsSelected", IsSelected.ToString());

            if (IsActive)
                writer.WriteAttributeString("IsActive", IsActive.ToString());

            if (IsLastFocusedDocument)
                writer.WriteAttributeString("IsLastFocusedDocument", IsLastFocusedDocument.ToString());
            
            if (!string.IsNullOrWhiteSpace(ContentId))
                writer.WriteAttributeString("ContentId", ContentId);
            
            if (_previousContainer != null)
            {
                var paneSerializable = _previousContainer as ILayoutPaneSerializable;
                writer.WriteAttributeString("PreviousContainerId", paneSerializable.Id);
                writer.WriteAttributeString("PreviousContainerIndex", _previousContainerIndex.ToString());
            }

        }
    }
}
