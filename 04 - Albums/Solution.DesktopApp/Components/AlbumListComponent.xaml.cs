namespace Solution.DesktopApp.Components;

public partial class AlbumListComponent : ContentView
{
    public static readonly BindableProperty AlbumProperty = BindableProperty.Create(
         propertyName: nameof(Album),
         returnType: typeof(AlbumModel),
         declaringType: typeof(AlbumListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public AlbumModel Album
    {
        get => (AlbumModel)GetValue(AlbumProperty);
        set => SetValue(AlbumProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(AlbumListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: nameof(CommandParameter),
         returnType: typeof(string),
         declaringType: typeof(AlbumListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Album", this.Album}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditAlbumView.Name, navigationQueryParameter);
    }
    public AlbumListComponent()
    {
        InitializeComponent();
    }
}