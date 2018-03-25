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

        // Gamepad Controls
        private static Buttons SprintButton = Buttons.A;
        private static Buttons DashButton = Buttons.B;
        private static Buttons LightAttackButton = Buttons.RightShoulder;
        private static Buttons HeavyAttackButton = Buttons.RightTrigger;

        // Keyboard Mouse Controls (attacks are handled with mouse button specific functions)
        private static Keys SprintKey = Keys.LeftShift;
        private static Keys DashKey = Keys.Space;

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

        public static bool WasMouseRightButtonPressed()
        {
            if (lastMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
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

            if (direction == Vector2.Zero)
            {
                return GetMovementDirection();
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
                return GetMovementDirection();
            }
            else
            {
                return Vector2.Normalize(direction);
            }
        }

        public static bool WasLightAttackPressed()
        {
            if (WasButtonPressed(LightAttackButton) || WasMouseLeftButtonPressed())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WasHeavyAttackPressed()
        {
            if (WasButtonPressed(HeavyAttackButton) || WasMouseRightButtonPressed())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WasDashPressed()
        {
            if (WasButtonPressed(DashButton) || WasKeyPressed(DashKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSprintPressed()
        {
            if (keyboardState.IsKeyDown(SprintKey) || gamepadState.IsButtonDown(SprintButton))
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
