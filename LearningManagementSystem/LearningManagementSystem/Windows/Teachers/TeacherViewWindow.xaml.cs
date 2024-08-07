﻿using System.Runtime.InteropServices;
using System;
using System.Windows;
using static LearningManagementSystem.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using LearningManagementSystem.Pages.Teachers;

namespace LearningManagementSystem.Windows.Teachers;

/// <summary>
/// Interaction logic for TeacherViewWindow.xaml
/// </summary>
public partial class TeacherViewWindow : Window
{
    public int index = 0;
    public TeacherViewWindow()
    {
        InitializeComponent();
    }

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    internal void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(this);

        var accent = new AccentPolicy();
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData();
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        data.SizeOfData = accentStructSize;
        data.Data = accentPtr;

        SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();

        if(index == 1)
        {
            TeacherViewPage teacherViewPage = new TeacherViewPage();
            PageNavigator.Content = teacherViewPage;
        }
        else if(index == 2)
        {
            TeacherUpdatePage teacherUpdatePage = new TeacherUpdatePage();
            PageNavigator.Content = teacherUpdatePage;
        }


    }
}
