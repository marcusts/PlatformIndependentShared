// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ViewModelBase.cs
// company="Marcus Technical Services, Inc.">
// </copyright>
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.PlatformIndependentShared.ViewModels
{
   using System.Threading.Tasks;
   using Com.MarcusTS.PlatformIndependentShared.Common.Interfaces;
   using Com.MarcusTS.ResponsiveTasks;
   using Com.MarcusTS.SharedUtils.Utils;

   public interface IViewModelBase :
      ITitledViewModel,
      IProvidePostBindingContextTasks_PI
   { }

   /// <summary>Much of the code is the same as that at the shape view with tasks.</summary>
   public class ViewModelBase_PI : PropertyChangedBase_PI, IViewModelBase
   {
      public ViewModelBase_PI()
      {
         CallPrepare();
      }

      public         IThreadSafeAccessor IsPostBindingContextCompleted     { get; set; } = new ThreadSafeAccessor(0);
      public         IResponsiveTasks    PostBindingContextTasks           { get; set; }
      public virtual int                 PostBindingContextTasksParamCount { get; set; } = 1;

      // The taskParams length must be the PostBindingContextTasksParamCount.
      public virtual async Task RunPostBindingContextTasks(params object[] taskParams)
      {
         IsPostBindingContextCompleted.SetFalse();

#if LOG
         await CallLog("FIRST").WithoutChangingContext();
#endif

         await PostBindingContextTasks.RunAllTasksUsingDefaults(taskParams).WithoutChangingContext();

#if LOG
         await CallLog("LAST").WithoutChangingContext();
#endif

         IsPostBindingContextCompleted.SetTrue();
      }

      protected virtual void Prepare()
      {
         PostBindingContextTasks = new ResponsiveTasks(PostBindingContextTasksParamCount);
      }

#if LOG
      private async Task CallLog(string suffix = "", [CallerMemberName] string memberName = "")
      {
         await LogUtils_PI.Log(GetType().ToString(), memberName, suffix).WithoutChangingContext();
      }
#endif

      private void CallPrepare()
      {
         Prepare();
      }

      public string Title { get; set; }
   }
}