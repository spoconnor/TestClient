﻿using System;
using amulware.Graphics;
//using TestClient.Meta;
//using TestClient.Rendering;
//using TestClient.Screens;
//using TestClient.Utilities.Console;
//using TestClient.Utilities.Input;
//using TestClient.Library;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using NLog;
using TestClient.Utilities.Input;
using TestClient.Rendering;
using TestClient.Screens;
using TestClient.Utilities.Console;
using System.ComponentModel;

namespace TestClient
{
    class TheGame : Program
    {
        private readonly Logger logger;

        private InputManager inputManager;
        private RenderContext renderContext;
        private ScreenManager screenManager;

        public TheGame(Logger logger)
            : base(1280, 720, GraphicsMode.Default, "TestClient",
                GameWindowFlags.Default, DisplayDevice.Default,
                3, 2, GraphicsContextFlags.Default)
        {
            this.logger = logger;
        }

        protected override void OnLoad(EventArgs e)
        {
            ConsoleCommands.Initialise();
            UserSettings.Load(logger);
            UserSettings.Save(logger);

            renderContext = new RenderContext();

            inputManager = new InputManager(Mouse);

            screenManager = new ScreenManager(inputManager);

            screenManager.AddScreenLayerOnTop(new StartScreen(screenManager, renderContext.Geometries, logger, inputManager));
            screenManager.AddScreenLayerOnTop(new ConsoleScreenLayer(screenManager, renderContext.Geometries, logger));

            KeyPress += (sender, args) => screenManager.RegisterPressedCharacter(args.KeyChar);

            UserSettings.SettingsChanged += () => OnResize(null);

            OnResize(EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            var viewportSize = new ViewportSize(Width, Height, UserSettings.Instance.UI.UIScale);
            screenManager.OnResize(viewportSize);
            renderContext.OnResize(viewportSize);
            base.OnResize(e);
        }

        protected override void OnUpdateUIThread()
        {
            inputManager.ProcessEventsAsync();
        }

        protected override void OnUpdate(UpdateEventArgs e)
        {
            inputManager.Update(Focused);

            if (inputManager.IsKeyPressed(Key.AltLeft) && inputManager.IsKeyHit(Key.F4))
            {
                Close();
            }

            screenManager.Update(e);
        }

        protected override void OnRender(UpdateEventArgs e)
        {
            renderContext.Compositor.PrepareForFrame();
            screenManager.Render(renderContext);
            renderContext.Compositor.FinalizeFrame();

            SwapBuffers();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            UserSettings.Save(logger);
            base.OnClosing(e);
        }
    }
}
