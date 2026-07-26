<frame layout="80%[400..] 80%[400..]" background={@Mods/StardewUI/Sprites/ControlBorder} padding="24">
  <scrollable peeking="128" scrollbar-margin="-18,0,0,0">
    <grid item-layout="length: 100+" layout="stretch content">
      <frame *repeat={:Labels} layout="100px 100px" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
        <label text={:Text} focusable="true" +hover:scale="1.5" +transition:scale="100ms EaseOutCubic"/>
      </frame>
    </grid>
  </scrollable>
</frame>
