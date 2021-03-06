using UnityEngine;
using System.Collections;

namespace Diamond.SkeletonDefense.Util
{
    public static class AnimatorExtension
    {
        public static void ResetAllAnimation(this Animator animator)
        {
            var paras = animator.parameters;
            foreach (var para in paras)
            {
                animator.ResetTrigger(para.name);
            }
        }
    }
}
