﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using amulware.Graphics;
using amulware.Graphics.ShaderManagement;
using OpenTK.Graphics.OpenGL;

namespace TestClient.Rendering
{
    class GameSurfaceManager
    {
        public Dictionary<string, IndexedSurface<UVColorVertexData>> Surfaces { get; }
            = new Dictionary<string, IndexedSurface<UVColorVertexData>>();
        
        public ReadOnlyCollection<Surface> SurfaceList { get; }
        
        public GameSurfaceManager(
            ShaderManager shaders,
            Matrix4Uniform view,
            Matrix4Uniform projection)
        {
            var hex = new IndexedSurface<UVColorVertexData>()
                .WithShader(shaders["Deferred/gSprite"])
                .AndSettings(
                    view, projection,
                    new TextureUniform("diffuseTexture", new Texture(sprite("hex-diffuse.png")), TextureUnit.Texture0),
                    new TextureUniform("normalTexture", new Texture(sprite("hex-normal.png")), TextureUnit.Texture1)
                    );
            Surfaces.Add("hex", hex);

            SurfaceList = Surfaces.Values.ToList<Surface>().AsReadOnly();
        }
        
        
        private static string asset(string path) => "Assets/" + path;
        private static string gfx(string path) => asset("Gfx/" + path);
        private static string sprite(string path) => gfx("Sprites/" + path);
    }
}
