using Terminal.Gui;

namespace PygmalionUI.Views;

public abstract class BaseView : FrameView
{
    protected BaseView(string title) : base(title)
    {
        X = 0;
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill();
    }

    protected Label CreateCenteredLabel(string text, int yOffset = 0)
    {
        return new Label(text)
        {
            X = Pos.Center(),
            Y = Pos.Center() + yOffset
        };
    }

    protected Button CreateCenteredButton(string text, int yOffset, Action onClick)
    {
        var button = new Button(text)
        {
            X = Pos.Center(),
            Y = Pos.Center() + yOffset
        };
        button.Clicked += () => onClick();
        return button;
    }
}
