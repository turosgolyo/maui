namespace Solution.DesktopApp.Components;

public partial class ArtistListComponent : ContentView
{
    public static readonly BindableProperty ArtistProperty = BindableProperty.Create(
         propertyName: nameof(Artist),
         returnType: typeof(ArtistModel),
         declaringType: typeof(ArtistListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public ArtistModel Artist
    {
        get => (ArtistModel)GetValue(ArtistProperty);
        set => SetValue(ArtistProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(ArtistListComponent),
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
         declaringType: typeof(ArtistListComponent),
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
            { "Artist", this.Artist}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditArtistView.Name, navigationQueryParameter);
    }
    public ArtistListComponent()
    {
        InitializeComponent();
    }
}