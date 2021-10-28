// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ResponsiveTaskHelper_PI.cs
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

// #define DEFEAT_TASK_WAITER
// #define DEFEAT_CANCEL_TOKEN

namespace Com.MarcusTS.PlatformIndependentShared.Common.Utils
{
   using System;
   using System.Diagnostics;
   using System.Threading;
   using System.Threading.Tasks;
   using Com.MarcusTS.PlatformIndependentShared.Common.Interfaces;
   using Com.MarcusTS.SharedUtils.Utils;

   /// <summary>
   /// Class TaskHelper.
   /// </summary>
   public static class ResponsiveTaskHelper_PI
   {
      public const int MAX_DELAY_MILLISECONDS = 10000;
      public const int MILLISECONDS_BETWEEN_DELAYS = 25;

      public static async Task SetBindingContextSafelyAndAwaitAllBranchingTasks(
         this ICanSetBindingContextContextSafely_PI view,
         object                                     context,
         int                                        maxDelay = MAX_DELAY_MILLISECONDS )
      {
         ErrorUtils.IssueArgumentErrorIfFalse( view.IsNotNullOrDefault(),    nameof( view )    + " required" );
         ErrorUtils.IssueArgumentErrorIfFalse( context.IsNotNullOrDefault(), nameof( context ) + " required" );
         var cancellationTokenSource = CreateCancellationTokenSource( maxDelay );

         // ReSharper disable once PossibleNullReferenceException
         // ReSharper disable once PossibleNullReferenceException
         await view.SetBindingContextSafely( context ).WithoutChangingContext();

         while
         (
            !cancellationTokenSource.Token.IsCancellationRequested
            &&
            (
               view.IsPostBindingContextCompleted.IsFalse()
               ||
               (
                  view.RunSubBindingContextTasksAfterAssignment
                  &&
                  context is IProvidePostBindingContextTasks_PI contextAsPostBindingContextTasksProvider
                  &&
                  contextAsPostBindingContextTasksProvider.IsPostBindingContextCompleted.IsFalse()
               )
            )
         )
         {
            Debug.WriteLine( nameof( SetBindingContextSafelyAndAwaitAllBranchingTasks ) + ": Waiting..." );
            await Task.Delay( MILLISECONDS_BETWEEN_DELAYS, cancellationTokenSource.Token ).WithoutChangingContext();
         }
      }

      public static async Task SetContentSafelyAndAwaitAllBranchingTasks<ViewT>
      (
         this ICanSetContentSafely_PI<ViewT> contentView,
         ViewT                               newContent = default,
         int                                 maxDelay   = MAX_DELAY_MILLISECONDS
      )
      {
         ErrorUtils.IssueArgumentErrorIfFalse( contentView.IsNotNullOrDefault(), nameof( contentView ) + " required" );

         // Content can be default; if so, the class must produce its own newContent

         var cancellationTokenSource = CreateCancellationTokenSource( maxDelay );

         // ReSharper disable once PossibleNullReferenceException
         await contentView.SetContentSafely( newContent ).WithoutChangingContext();

         // ReSharper disable once PossibleNullReferenceException
         while ( !cancellationTokenSource.Token.IsCancellationRequested &&
                 contentView.IsPostContentCompleted.IsFalse() )
         {
            Debug.WriteLine( nameof( SetContentSafelyAndAwaitAllBranchingTasks ) + ": Waiting..." );
            await Task.Delay( MILLISECONDS_BETWEEN_DELAYS, cancellationTokenSource.Token ).WithoutChangingContext();
         }
      }

      private static CancellationTokenSource CreateCancellationTokenSource( int maxDelay )
      {
         var cancellationTokenSource = new CancellationTokenSource();
         cancellationTokenSource.CancelAfter( TimeSpan.FromMilliseconds( maxDelay ) );
         return cancellationTokenSource;
      }
   }
}