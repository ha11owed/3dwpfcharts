using System.Windows.Input;

namespace WpfPlot.Plot3d
{
    public enum KeyState
    {
        None,
        Up,
        Down,
    }

    public class KeyboardInput
    {
        public KeyboardInput()
        {
            Key = Key.None;
            KeyState = KeyState.None;
            ModifierKeys = ModifierKeys.None;
        }

        public KeyboardInput(Key key, KeyState keyState, ModifierKeys modifierKeys)
        {
            Key = key;
            KeyState = keyState;
            ModifierKeys = modifierKeys;
        }

        public Key Key { get; private set; }
        public KeyState KeyState { get; private set; }
        public ModifierKeys ModifierKeys { get; private set; }
    }
}