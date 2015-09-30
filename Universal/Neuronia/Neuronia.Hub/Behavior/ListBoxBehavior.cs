using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Neuronia.Hub.Behavior
{
    public static class GridViewBehavior
    {
        private static readonly DependencyProperty SelectedItemsBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItemsBehavior",
                typeof(SelectedItemsBehavior),
                typeof(GridView),
                null);

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
                "SelectedItems",
                typeof(IList),
                typeof(GridViewBehavior),
                new PropertyMetadata(null, ItemsPropertyChanged));

        public static void SetSelectedItems(GridView listBox, IList list)
        {
            listBox.SetValue(SelectedItemsProperty, list);
        }

        public static IList GetSelectedItems(GridView listBox)
        {
            return listBox.GetValue(SelectedItemsProperty) as IList;
        }

        private static void ItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as GridView;
            if (target != null)
            {
                GetOrCreateBehavior(target, e.NewValue as IList);
            }
        }

        private static SelectedItemsBehavior GetOrCreateBehavior(GridView target, IList list)
        {
            var behavior = target.GetValue(SelectedItemsBehaviorProperty) as SelectedItemsBehavior;
            if (behavior == null)
            {
                behavior = new SelectedItemsBehavior(target, list);
                target.SetValue(SelectedItemsBehaviorProperty, behavior);
            }

            return behavior;
        }

        private class SelectedItemsBehavior
        {
            private readonly GridView _listBox;
            private readonly IList _boundList;
            private bool _listBoxSelectionChanging = false;
            private bool _collectionChanging = false;

            public SelectedItemsBehavior(GridView listBox, IList boundList)
            {
                _boundList = boundList;
                if (_boundList is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged)_boundList).CollectionChanged += SelectedItemsBehavior_CollectionChanged;
                }

                _listBox = listBox;
                _listBox.SelectionChanged += OnSelectionChanged;
            }

            private void SelectedItemsBehavior_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (_listBoxSelectionChanging == false)
                {
                    _collectionChanging = true;

                    _listBox.SelectedItems.Clear();
                    foreach (var item in _boundList)
                    {
                        _listBox.SelectedItems.Add(item);
                    }

                    _collectionChanging = false;
                }
            }

            private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (_collectionChanging == false)
                {
                    _listBoxSelectionChanging = true;

                    _boundList.Clear();
                    foreach (var item in _listBox.SelectedItems)
                    {
                        _boundList.Add(item);
                    }

                    _listBoxSelectionChanging = false;
                }
            }
        }
    }
}
