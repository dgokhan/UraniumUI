﻿using InputKit.Shared;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using UraniumUI.Extensions;

namespace UraniumUI.Material.Controls;
public partial class DataGrid
{
    public IList ItemsSource { get => (IList)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(DataGrid), propertyChanged: (bo, ov, nv) => (bo as DataGrid).OnItemSourceSet(ov as IList, nv as IList));

    public DataTemplate CellItemTemplate { get => (DataTemplate)GetValue(CellItemTemplateProperty); set => SetValue(CellItemTemplateProperty, value); }

    public static readonly BindableProperty CellItemTemplateProperty =
        BindableProperty.Create(nameof(CellItemTemplate), typeof(DataTemplate), typeof(DataGrid), defaultValue: null,
            propertyChanged: (bo, ov, nv) => (bo as DataGrid).Render());

    public DataTemplate TitleTemplate { get => (DataTemplate)GetValue(TitleTemplateProperty); set => SetValue(TitleTemplateProperty, value); }

    public static readonly BindableProperty TitleTemplateProperty =
        BindableProperty.Create(nameof(TitleTemplate), typeof(DataTemplate), typeof(DataGrid), defaultValue: null,
            propertyChanged: (bo, ov, nv) => (bo as DataGrid).OnTitleTemplateChanged());

    public Color LineSeperatorColor { get => (Color)GetValue(LineSeperatorColorProperty); set => SetValue(LineSeperatorColorProperty, value); }

    public static readonly BindableProperty LineSeperatorColorProperty =
        BindableProperty.Create(nameof(LineSeperatorColor), typeof(Color), typeof(DataGrid), defaultValue: Colors.Gray,
            propertyChanged: (bo, ov, nv) => (bo as DataGrid).OnPropertyChanged(nameof(LineSeperatorColor)));

    public bool UseAutoColumns { get => (bool)GetValue(UseAutoColumnsProperty); set => SetValue(UseAutoColumnsProperty, value); }

    public static readonly BindableProperty UseAutoColumnsProperty =
        BindableProperty.Create(nameof(UseAutoColumns), typeof(bool), typeof(DataGrid), defaultValue: false,
            propertyChanged: (bo, ov, nv) => (bo as DataGrid).SetAutoColumns());

    public IList SelectedItems { get => (IList)GetValue(SelectedItemsProperty); set => SetValue(SelectedItemsProperty, value); }

    public static readonly BindableProperty SelectedItemsProperty =
        BindableProperty.Create(nameof(SelectedItems), typeof(IList), typeof(DataGrid), defaultValue: new ObservableCollection<object>(),
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bo, ov, nv) => (bo as DataGrid).OnSelectedItemsSet());

    public Color SelectionColor { get => (Color)GetValue(SelectionColorProperty); set => SetValue(SelectionColorProperty, value); }

    public static readonly BindableProperty SelectionColorProperty =
        BindableProperty.Create(nameof(SelectionColor), typeof(Color), typeof(DataGrid), defaultValue: InputKitOptions.GetAccentColor(),
            propertyChanged: (bo, ov, nv) => (bo as DataGrid).SetSelectionVisualStatesForAll());

    public View EmptyView { get => (View)GetValue(EmptyViewProperty); set => SetValue(EmptyViewProperty, value); }

    public static readonly BindableProperty EmptyViewProperty =
        BindableProperty.Create(nameof(EmptyView), typeof(View), typeof(DataGrid));

    public DataTemplate EmptyViewTemplate { get => (DataTemplate)GetValue(EmptyViewTemplateProperty); set => SetValue(EmptyViewTemplateProperty, value); }

    public static readonly BindableProperty EmptyViewTemplateProperty =
        BindableProperty.Create(nameof(EmptyViewTemplate), typeof(DataTemplate), typeof(DataGrid),
            propertyChanged: (bo, ov, nv) => (bo as DataGrid).OnEmptyViewTemplateSet());

    public static readonly BindableProperty IsSelectedProperty = BindableProperty.CreateAttached(
        "IsSelected",
        typeof(bool),
        typeof(View),
        false, propertyChanged: (bo, ov, nv) =>
        {
            if (ov == nv)
            {
                return;
            }

            IsSelectedChanged?.Invoke(bo, (bool)nv);

            bo.NotifyPropertyChanged("IsSelected");
        });

    public static bool GetIsSelected(BindableObject target)
    {
        return (bool)target.GetValue(IsSelectedProperty);
    }

    public static void SetIsSelected(BindableObject target, bool value)
    {
        target.SetValue(IsSelectedProperty, value);
    }

    public static event EventHandler<bool> IsSelectedChanged;
}