<lane orientation="vertical">
  <banner text={PrimaryItemCount} />
  <frame layout="80%[400..] 80%[400..]" background={@Mods/StardewUI/Sprites/ControlBorder} padding="24">
    <scrollable peeking="128" scrollbar-margin="-18,0,0,0">
      <grid item-layout="count: 3" layout="stretch content" primary-item-count={>PrimaryItemCount}>
        <frame focusable="true" layout="stretch 100px" item-span="2" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
          <label text="2" +hover:opacity="0.5"/>
        </frame>
        <frame focusable="true" layout="stretch 100px" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
          <label text="1" +hover:opacity="0.5"/>
        </frame>
        <frame focusable="true" layout="stretch 100px" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
          <label text="1" +hover:opacity="0.5"/>
        </frame>
        <frame focusable="true" layout="stretch 100px" item-span="-1" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
          <label text="+" +hover:opacity="0.5"/>
        </frame>
        <frame focusable="true" layout="stretch 100px" item-span="-1" background={@Mods/StardewUI/Sprites/ButtonDark} horizontal-content-alignment="middle" vertical-content-alignment="middle">
          <label text="+" +hover:opacity="0.5"/>
        </frame>
      </grid>
    </scrollable>
  </frame>
</lane>
