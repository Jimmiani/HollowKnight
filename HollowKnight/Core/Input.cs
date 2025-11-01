using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollowKnight.Core
{
    public static class Input
    {
        private static KeyboardState _currentKeyboard;
        private static KeyboardState _previousKeyboard;

        private static MouseState _currentMouse;
        private static MouseState _previousMouse;

        public static void Update()
        {
            _previousKeyboard = _currentKeyboard;
            _previousMouse = _currentMouse;

            _currentKeyboard = Keyboard.GetState();
            _currentMouse = Mouse.GetState();
        }

        // --- Keyboard ---
        public static bool IsKeyDown(Keys key) => _currentKeyboard.IsKeyDown(key);
        public static bool IsKeyUp(Keys key) => _currentKeyboard.IsKeyUp(key);

        public static bool IsKeyPressed(Keys key) =>
            _currentKeyboard.IsKeyDown(key) && _previousKeyboard.IsKeyUp(key);

        public static bool IsKeyReleased(Keys key) =>
            _currentKeyboard.IsKeyUp(key) && _previousKeyboard.IsKeyDown(key);

        // --- Mouse ---
        public static Point MousePosition => _currentMouse.Position;

        public static bool IsLeftClick() =>
            _currentMouse.LeftButton == ButtonState.Pressed &&
            _previousMouse.LeftButton == ButtonState.Released;

        public static bool IsRightClick() =>
            _currentMouse.RightButton == ButtonState.Pressed &&
            _previousMouse.RightButton == ButtonState.Released;

        public static bool IsLeftHeld() => _currentMouse.LeftButton == ButtonState.Pressed;
        public static bool IsRightHeld() => _currentMouse.RightButton == ButtonState.Pressed;
    }
}
