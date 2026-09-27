namespace OrderManagerMaui.Theme;

public enum AppColor
{
    Primary,
    Secondary,
    Success,
    Warning,
    Danger,
    DangerSubtle,
    Background,
    Surface,
    Border,
    Divider,
    TextPrimary,
    TextSecondary,
    TextMuted,
    TextSubtle
}

public static class AppColorExtensions
{
    public static Color ToColor(this AppColor color) => color switch
    {
        AppColor.Primary => Color.FromArgb("#4F46E5"),
        AppColor.Secondary => Color.FromArgb("#0284C7"),
        AppColor.Success => Color.FromArgb("#16A34A"),
        AppColor.Warning => Colors.Goldenrod,
        AppColor.Danger => Color.FromArgb("#EF4444"),
        AppColor.DangerSubtle => Color.FromArgb("#FEE2E2"),
        AppColor.Background => Color.FromArgb("#F8FAFC"),
        AppColor.Surface => Colors.White,
        AppColor.Border => Color.FromArgb("#E2E8F0"),
        AppColor.Divider => Color.FromArgb("#F1F5F9"),
        AppColor.TextPrimary => Color.FromArgb("#0F172A"),
        AppColor.TextSecondary => Color.FromArgb("#475569"),
        AppColor.TextMuted => Color.FromArgb("#64748B"),
        AppColor.TextSubtle => Color.FromArgb("#94A3B8"),
        _ => Colors.Transparent
    };
}
