<frame layout="80%[400..] 80%[400..]" background={@Mods/StardewUI/Sprites/ControlBorder} padding="24" left-click=|NextOuter()|>
  <lane *context={CurrOuter}>
    <banner background={@Mods/StardewUI/Sprites/BannerBackground}
      background-border-thickness="48,0"
      text={:Label}
      left-click=|NextInner()|/>
    <banner background={@Mods/StardewUI/Sprites/BannerBackground}
      background-border-thickness="48,0"
      text="Show"
      left-click=|ToggleInner()|/>
    <frame *if={ShowInner} *context={CurrInner} layout="100px 100px" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
      <label text={:Label} focusable="true" +transition:scale="100ms EaseOutCubic"/>
    </frame>
  </lane>
</frame>
