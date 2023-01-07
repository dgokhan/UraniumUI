using System.Reflection;

namespace UraniumUI.Extensions;
public static class NotifyPropertyChangedExtensions
{
    private static MethodInfo _method;
    static NotifyPropertyChangedExtensions()
    {
        _method = typeof(BindableObject).GetMethod("OnPropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance);
    }
    public static void NotifyPropertyChanged(this BindableObject source, string propertyName)
    {
        _method?.Invoke(source, new[] { propertyName });
    }
}
