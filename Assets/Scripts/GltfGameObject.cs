using System.Threading.Tasks;
using GLTFast.Loading;
using UnityEngine;
using UnityEngine.Events;

namespace GLTFast
{
    public class GltfGameObject  : GltfAsset
    {

        [SerializeField] private UnityEvent onLoadingStarted, onLoadingSuccess;
        [SerializeField] private UnityEvent<string> onLoadingFailed;
        [SerializeField] private string loadingErrorMessage = "loading error";
       
        protected override async void Start() {
            base.Start();
        }

        public override async Task<bool> Load(
            string url,
            IDownloadProvider downloadProvider=null,
            IDeferAgent deferAgent=null,
            IMaterialGenerator materialGenerator=null,
            ICodeLogger logger = null
            )
        {
            onLoadingStarted.Invoke();
            logger ??= new ConsoleLogger();
            var success = await base.Load(url, downloadProvider, deferAgent, materialGenerator, logger);
            if(success) {
                if (deferAgent != null) await deferAgent.BreakPoint();
                
                onLoadingSuccess.Invoke();
                // Auto-Instantiate
                if (sceneId>=0) {
                    InstantiateScene(sceneId,logger);
                } else {
                    Instantiate(logger);
                }
            }
            else
            {
                onLoadingFailed.Invoke(loadingErrorMessage);
            }
            return success;
        }
    }
}