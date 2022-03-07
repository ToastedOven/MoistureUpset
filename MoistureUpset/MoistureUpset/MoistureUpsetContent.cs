using System.Collections;
using RoR2.ContentManagement;

namespace MoistureUpset
{
    public class MoistureUpsetContent : IContentPackProvider
    {
        public static MoistureUpsetContent Content { get; private set; }
        
        private ContentPack _contentPack;

        internal static void Init()
        {
            Content = new MoistureUpsetContent();
            
            ContentManager.collectContentPackProviders += AddContentPack;
        }

        private static void AddContentPack(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(Content);
        }

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(_contentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        public string identifier => Moisture_Upset.Guid;
    }
}