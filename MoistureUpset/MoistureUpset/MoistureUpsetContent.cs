using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API;
using RoR2.ContentManagement;
using UnityEngine;

namespace MoistureUpset
{
    public class MoistureUpsetContent : IContentPackProvider
    {
        public string identifier => "MoistureUpset.Content";

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            contentPack.unlockableDefs.Add(GenerateUnlockableDefs());

            args.ReportProgress(1f);
            yield break;
        }
        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(contentPack, args.output);

            args.ReportProgress(1f);
            yield break;
        }

        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }

        private UnlockableDef[] GenerateUnlockableDefs()
        {
            List<UnlockableDef> unlockableDefs = new List<UnlockableDef>();

            UnlockableDef BonziBuddyUnlockDef = ScriptableObject.CreateInstance<UnlockableDef>();
            AchievementDef achievementDef = new AchievementDef
            {
                identifier = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID",
                unlockableRewardIdentifier = "MOISTURE_BONZIBUDDY_REWARD_ID",
                prerequisiteAchievementIdentifier = "MOISTURE_BONZIBUDDY_PREREQ_ID",
                nameToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME",
                descriptionToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC",
                iconPath = "@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/BonziIcon.png",
            };
            BonziBuddyUnlockDef.nameToken = "MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME";

            BonziBuddyUnlockDef.cachedName = "BonziBuddyUnlock";

            BonziBuddyUnlockDef.hidden = false;

            BonziBuddyUnlockDef.sortScore = 420;

            BonziBuddyUnlockDef.index = (UnlockableIndex) 69420;

            BonziBuddyUnlockDef.getHowToUnlockString = (() => Language.GetStringFormatted("UNLOCK_VIA_ACHIEVEMENT_FORMAT", new object[]
            {
                    Language.GetString(achievementDef.nameToken),
                    Language.GetString(achievementDef.descriptionToken)
            }));
            BonziBuddyUnlockDef.getUnlockedString = (() => Language.GetStringFormatted("UNLOCKED_FORMAT", new object[]
            {
                    Language.GetString(achievementDef.nameToken),
                    Language.GetString(achievementDef.descriptionToken)
            }));
            
            unlockableDefs.Add(BonziBuddyUnlockDef);

            return unlockableDefs.ToArray();
        }

        private ContentPack contentPack = new ContentPack();
    }
}
