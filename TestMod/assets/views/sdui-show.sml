<frame layout="80%[400..] 80%[400..]" background={@Mods/StardewUI/Sprites/ControlBorder} padding="24">
  <lane orientation="vertical">
    <dropdown layout="400px content" option-min-width="200" options={:DropdownOptions} selected-option={<>SelectedOption} />
    <textinput text={<>Text} selected-text={>Selected} placeholder="textinput" />
    <label text={Selected}/>
  </lane>
</frame>
