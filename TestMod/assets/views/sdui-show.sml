<frame background={@Mods/StardewUI/Sprites/ControlBorder} padding="24">
  <lane orientation="vertical">
    <checkbox is-checked={<>IsChecked}/>
    <label
      text={#Example.Form.Button.OK}
      +state:checked={<IsChecked}
      +state:checked:text={#Example.Form.Button.Cancel}
    />
  </lane>
</frame>
