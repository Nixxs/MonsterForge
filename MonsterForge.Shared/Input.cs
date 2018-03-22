using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterForge
{
    static class Input
    {
        private static KeyboardState keyboardState;
        private static KeyboardState lastKeyboardState;
        private static MouseState mouseState;
        private static MouseState lastMouseState;
        private static GamePadState gamepadState;
        private static GamePadState lastGamepadState;

        private static bool isAimingWithMouse = false;

        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(mouseState.X, mouseState.Y);
            }
        }

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            lastGamepadState = gamepadState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);

            // if any of the keyboard aiming keys of the control pad right sitck is being used then
            // set isAimingWithMouse to false otherwise if the mouse is moving then set it to true
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Right) || 
                keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down) ||
                gamepadState.ThumbSticks.Right != Vector2.Zero)
            {
                isAimingWithMouse = false;
            } else if (MousePosition != new Vector2(lastMouseState.X, lastMouseState.Y))
            {
                isAimingWithMouse = true;
            }
        }

        public static bool WasKeyPressed(Keys key)
        {
            if (lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static bool WasMouseLeftButtonPressed()
        {
            if (lastMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WasButtonPressed(Buttons button)
        {
            if (lastGamepadState.IsButtonUp(button) && gamepadState.IsButtonDown(button))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static Vector2 GetMovementDirection()
        {
            Vector2 direction = gamepadState.ThumbSticks.Left;
            direction.Y = direction.Y * -1; // invert the y-axis direction

            if (keyboardState.IsKeyDown(Keys.A))
            {
                direction.X -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                direction.X += 1;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                direction.Y -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                direction.Y += 1;
            }

            if (direction.LengthSquared() > 1)
            {
                direction.Normalize();
            }

            return direction;
        }

        public static Vector2 GetAimDirection()
        {
            if (isAimingWithMouse)
            {
                return GetMouseAimDirection();
            }

            Vector2 direction = gamepadState.ThumbSticks.Right;
            direction.Y = direction.Y * -1; // invert the y-axis direction

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                direction.Y -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                direction.Y += 1;
            }

            if (direction == Vector2.Zero)
            {
                return Vector2.Zero;
            }
            else
            {
                return Vector2.Normalize(direction);
            }
        }

        private static Vector2 GetMouseAimDirection()
        {
            Vector2 direction = MousePosition - PlayerChar.Instance.Position;

            if (direction == Vector2.Zero)
            {
                return Vector2.Zero;
            }
            else
            {
                return Vector2.Normalize(direction);
            }
        }

        public static bool WasLightAttackPressed()
        {
            if (WasButtonPressed(Buttons.RightShoulder) || WasMouseLeftButtonPressed() || WasKeyPressed(Keys.Space))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
