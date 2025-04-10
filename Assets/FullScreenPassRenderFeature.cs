using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullScreenPassRenderFeature : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public Material blitMaterial;
        private RTHandle cameraColorTargetHandle;

        public void Setup(RTHandle cameraColorTargetHandle)
        {
            this.cameraColorTargetHandle = cameraColorTargetHandle;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (blitMaterial == null) return;

            CommandBuffer cmd = CommandBufferPool.Get("FullScreenPass");

            Blit(cmd, cameraColorTargetHandle, cameraColorTargetHandle, blitMaterial, 0); // 0 = pass index

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    [SerializeField] private Material material;

    private CustomRenderPass renderPass;

    
    public Material OutlineMaterial => material;

    public override void Create()
    {
        renderPass = new CustomRenderPass
        {
            blitMaterial = material,
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderPass.Setup(renderer.cameraColorTargetHandle);
        renderer.EnqueuePass(renderPass);
    }
}

