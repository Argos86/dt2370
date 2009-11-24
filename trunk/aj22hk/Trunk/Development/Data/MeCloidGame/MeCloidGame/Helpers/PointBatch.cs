using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Helpers
{
    public sealed class PointBatch
    {
        private GraphicsDevice m_graphicsDevice;
        private List<VertexPositionColor> m_points = new List<VertexPositionColor>();
        private VertexDeclaration m_vertexDeclaration;
        private BasicEffect m_basicEffect;
        private int m_pointSize;

        public PointBatch(GraphicsDevice a_graphicsDevice, float a_alpha, int a_pointSize)
        {
            m_graphicsDevice = a_graphicsDevice;
            m_basicEffect = new BasicEffect(m_graphicsDevice, null);
            m_basicEffect.VertexColorEnabled = true;
            m_basicEffect.Alpha = a_alpha;
            m_basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0.0f, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height, 0.0f, 0.0f, -1.0f);
            m_basicEffect.View = Matrix.Identity;
            m_basicEffect.World = Matrix.Identity;

            m_vertexDeclaration = new VertexDeclaration(m_graphicsDevice, VertexPositionColor.VertexElements);

            m_pointSize = a_pointSize;
        }

        public void Begin()
        {
            m_points.Clear();
        }

        public void Batch(Vector2 a_point, Color a_color)
        {
            VertexPositionColor batchPoint = new VertexPositionColor(new Vector3(a_point, 0.0f), a_color);

            m_points.Add(batchPoint);
        }

        public void End()
        {
            if (m_points.Count > 0)
            {
                m_graphicsDevice.VertexDeclaration = m_vertexDeclaration;
                m_graphicsDevice.RenderState.PointSize = m_pointSize;
                m_graphicsDevice.RenderState.FillMode = FillMode.Solid;

                m_basicEffect.Begin();

                foreach (EffectPass pass in m_basicEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    m_graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.PointList, m_points.ToArray(), 0, m_points.Count);

                    pass.End();
                }

                m_basicEffect.End();
            }
        }
    }
}