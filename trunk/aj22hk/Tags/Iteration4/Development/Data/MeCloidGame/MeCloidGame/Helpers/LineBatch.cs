using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Helpers
{
    public sealed class LineBatch
    {
        private GraphicsDevice m_graphicsDevice;
        private List<VertexPositionColor> m_points = new List<VertexPositionColor>();
        private List<short> m_indices = new List<short>();
        private VertexDeclaration m_vertexDeclaration;
        private BasicEffect m_basicEffect;

        public LineBatch(GraphicsDevice a_graphicsDevice, float a_alpha)
        {
            m_graphicsDevice = a_graphicsDevice;

            m_basicEffect = new BasicEffect(m_graphicsDevice, null);
            m_basicEffect.VertexColorEnabled = true;
            m_basicEffect.Alpha = a_alpha;
            m_basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0.0f, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height, 0.0f, 0.0f, -1.0f);
            m_basicEffect.View = Matrix.Identity;
            m_basicEffect.World = Matrix.Identity;

            m_vertexDeclaration = new VertexDeclaration(m_graphicsDevice, VertexPositionColor.VertexElements);
        }

        public void Begin()
        {
            m_points.Clear();
            m_indices.Clear();
        }

        public void Batch(Vector2 a_startPoint, Vector2 a_endPoint, Color a_color, float a_layerDepth)
        {
            Batch(a_startPoint, a_color, a_layerDepth);
            Batch(a_endPoint, a_color, a_layerDepth);
        }

        public void Batch(Vector2 a_startPoint, Color a_startColor, Vector2 a_endPoint, Color a_endColor, float a_layerDepth)
        {
            Batch(a_startPoint, a_startColor, a_layerDepth);
            Batch(a_endPoint, a_endColor, a_layerDepth);
        }

        public void Batch(Vector2 a_point, Color a_color, float a_layerDepth)
        {
            VertexPositionColor batchPoint = new VertexPositionColor(new Vector3(a_point, a_layerDepth), a_color);
            m_points.Add(batchPoint);

            m_indices.Add((short)m_indices.Count);
        }

        public void End()
        {
            if (m_points.Count > 0)
            {
                m_graphicsDevice.VertexDeclaration = m_vertexDeclaration;
                m_graphicsDevice.RenderState.FillMode = FillMode.Solid;

                m_basicEffect.Begin();

                foreach (EffectPass pass in m_basicEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    m_graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.LineList, m_points.ToArray(), 0, m_points.Count, m_indices.ToArray(), 0, m_points.Count / 2);

                    pass.End();
                }

                m_basicEffect.End();
            }
        }
    }
}