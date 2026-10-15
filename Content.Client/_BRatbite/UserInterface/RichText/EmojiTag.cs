using System.Diagnostics.CodeAnalysis;
using Content.Shared._BRatbite.UserInterface;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.RichText;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Client._BRatbite.UserInterface.RichText;

[UsedImplicitly]
public sealed class EmojiTag : IMarkupTagHandler
{
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly IEntityManager _entMan = default!;

    private const int EmojiWidth = 16;
    private const int EmojiHeight = 16;

    public string Name => "emoji";

    public bool TryCreateControl(MarkupNode node, [NotNullWhen(true)] out Control? control)
    {
        control = null;
        if (!node.Value.TryGetString(out var id) || !_proto.TryIndex<EmojiPrototype>(id, out var emoji))
            return false;
        var view = new TextureRect
        {
            SetWidth = EmojiWidth,
            SetHeight = EmojiHeight,
            Texture = _entMan.System<SpriteSystem>().Frame0(emoji.Sprite),
            Stretch = TextureRect.StretchMode.Scale,
            HorizontalAlignment = Control.HAlignment.Center,
            VerticalAlignment = Control.VAlignment.Center,
            Margin = new Thickness(2),
        };
        control = view;
        return true;
    }
}
