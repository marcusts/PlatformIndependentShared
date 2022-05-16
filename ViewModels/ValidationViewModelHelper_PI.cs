// *********************************************************************************
// Copyright @2022 Marcus Technical Services, Inc.
// <copyright
// file=ValidationViewModelHelper_PI.cs
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
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using Com.MarcusTS.PlatformIndependentShared.Common.Interfaces;
   using Com.MarcusTS.ResponsiveTasks;
   using Com.MarcusTS.SharedUtils.Utils;

   public interface IHaveValidationViewModelHelper
   {
      IValidationViewModelHelper ValidationHelper { get; set; }
   }

   public interface IValidationViewModelHelper : IPropertyChangedBase_PI
   {
      bool MultipleSubHelpersMustAllValidateTrue { get; set; }
      bool PageIsNeverValid                      { get; set; }
      bool PageIsValid                           { get; set; }

      IResponsiveTasks PageIsValidChangedTask { get; set; }

      bool ProceedWithoutPropertyChanges { get; set; }
      bool ValidatesTrueWhenEmpty        { get; set; }

      Task AddBehaviors(ICanBeValid_PI[] behaviors);

      void AddSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers);

      ICanBeValid_PI[] GetBehaviors();

      void KillBehaviors();

      Task RemoveBehaviorsWithoutNotification(ICanBeValid_PI[] behaviors);

      void RemoveSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers);

      Task RevalidateBehaviors(bool forceAll);

      Task RevalidateBehaviors();

      Task SetMultipleSubHelpersMustAllValidateTrue(bool setMultipleSubHelpersMustAllValidateTrue);

      Task SetPageIsNeverValid(bool isNeverValid);

      Task SetPageIsValid(bool isValid);

      Task SetProceedWithoutPropertyChanges(bool proceedWithoutPropertyChanges);

      Task<bool> SetValidatesTrueWhenEmpty(bool validatesTrueWhenEmpty);
   }

   public class ValidationViewModelHelper_PI : PropertyChangedBase_PI, IValidationViewModelHelper
   {
      private readonly IList<ICanBeValid_PI> _behaviors = new List<ICanBeValid_PI>();

      private readonly IList<IHaveValidationViewModelHelper> _subViewModelHelpers =
         new List<IHaveValidationViewModelHelper>();

      private volatile bool _multipleSubHelpersMustAllValidateTrue;
      private volatile bool _pageIsNeverValid;
      private volatile bool _pageIsValid;
      private volatile bool _proceedWithoutPropertyChanges;
      private volatile bool _revalidateBehaviorsEntered;
      private volatile bool _validatesTrueWhenEmpty;

      public ValidationViewModelHelper_PI()
      {
         SetPageIsValid(ValidatesTrueWhenEmpty).FireAndFuhgetAboutIt();
      }

      public bool MultipleSubHelpersMustAllValidateTrue
      {
         get => _multipleSubHelpersMustAllValidateTrue;
         set =>

            // important Improper TPL root
            SetMultipleSubHelpersMustAllValidateTrue(value).FireAndFuhgetAboutIt();
      }

      public bool PageIsNeverValid
      {
         get => _pageIsNeverValid;
         set =>

            // important Improper TPL root
            SetPageIsNeverValid(value).FireAndFuhgetAboutIt();
      }

      public bool PageIsValid
      {
         get => _pageIsValid;
         set =>

            // important Improper TPL root
            SetPageIsValid(value).FireAndFuhgetAboutIt();
      }

      public IResponsiveTasks PageIsValidChangedTask { get; set; } = new ResponsiveTasks();

      public bool ProceedWithoutPropertyChanges
      {
         get => _proceedWithoutPropertyChanges;
         set =>

            // important Improper TPL root
            SetProceedWithoutPropertyChanges(value).FireAndFuhgetAboutIt();
      }

      public bool ValidatesTrueWhenEmpty
      {
         get => _validatesTrueWhenEmpty;
         set =>

            // important Improper TPL root
            SetValidatesTrueWhenEmpty(value).FireAndFuhgetAboutIt();
      }

      public async Task AddBehaviors(ICanBeValid_PI[] behaviors)
      {
         foreach (var behavior in behaviors)
         {
            if (!_behaviors.Contains(behavior))
            {
               behavior.IsValidChangedTask.AddIfNotAlreadyThere(this, HandleIsValidChangedTask);
               _behaviors.Add(behavior);
            }
         }

         await RevalidateBehaviors().WithoutChangingContext();
      }

      public void AddSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers)
      {
         foreach (var helper in helpers)
         {
            _subViewModelHelpers.Add(helper);
         }
      }

      public ICanBeValid_PI[] GetBehaviors()
      {
         return _behaviors.ToArray();
      }

      public void KillBehaviors()
      {
         _behaviors.Clear();
      }

      public Task RemoveBehaviorsWithoutNotification(ICanBeValid_PI[] behaviors)
      {
         foreach (var behavior in behaviors)
         {
            if (_behaviors.Contains(behavior))
            {
               _behaviors.Remove(behavior);
            }
         }

         return Task.CompletedTask;
      }

      public void RemoveSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers)
      {
         foreach (var helper in helpers)
         {
            if (_subViewModelHelpers.Contains(helper))
            {
               _subViewModelHelpers.Remove(helper);
            }
         }
      }

      public async Task RevalidateBehaviors(bool forceAll)
      {
         if (_revalidateBehaviorsEntered)
         {
            return;
         }

         _revalidateBehaviorsEntered = true;

         if (_subViewModelHelpers.IsNotEmpty())
         {
            await SetPageIsValid(
               (ValidatesTrueWhenEmpty && _subViewModelHelpers.IsEmpty())
             ||
               (MultipleSubHelpersMustAllValidateTrue &&
                  _subViewModelHelpers.All(h => h.ValidationHelper.PageIsValid))
             ||
               (!MultipleSubHelpersMustAllValidateTrue &&
                  _subViewModelHelpers.Any(h => h.ValidationHelper.PageIsValid))
            ).WithoutChangingContext();
         }
         else
         {
            if (forceAll)
            {
               // Run through all behaviors; ask to validate; respond only once at the end
               if (_behaviors.IsNotEmpty())
               {
                  foreach (var behavior in _behaviors)
                  {
                     if (behavior is IValidationBehaviorBase_PI behaviorAsValidationHost)
                     {
                        await behaviorAsValidationHost.RevalidateAllConditions().WithoutChangingContext();
                     }
                  }
               }
            }

            await SetPageIsValid(_behaviors.IsEmpty() || _behaviors.All(b => b.IsValid.IsTrue()))
              .WithoutChangingContext();
         }

         _revalidateBehaviorsEntered = false;
      }

      public async Task RevalidateBehaviors()
      {
         await RevalidateBehaviors(true).WithoutChangingContext();
      }

      public async Task SetMultipleSubHelpersMustAllValidateTrue(bool setMultipleSubHelpersMustAllValidateTrue)
      {
         if (_multipleSubHelpersMustAllValidateTrue != setMultipleSubHelpersMustAllValidateTrue)
         {
            _multipleSubHelpersMustAllValidateTrue = setMultipleSubHelpersMustAllValidateTrue;
            await RevalidateBehaviors().WithoutChangingContext();
         }
      }

      public async Task SetPageIsNeverValid(bool isNeverValid)
      {
         if (_pageIsNeverValid != isNeverValid)
         {
            _pageIsNeverValid = isNeverValid;

            if (_pageIsNeverValid)
            {
               await SetPageIsValid(false).WithoutChangingContext();
            }
         }
      }

      public async Task SetPageIsValid(bool isValid)
      {
         if ((_pageIsValid != isValid) && !PageIsNeverValid)
         {
            _pageIsValid = isValid;
            await PageIsValidChangedTask.RunAllTasksUsingDefaults().WithoutChangingContext();
         }
      }

      public async Task SetProceedWithoutPropertyChanges(bool proceedWithoutPropertyChanges)
      {
         if (_proceedWithoutPropertyChanges != proceedWithoutPropertyChanges)
         {
            _proceedWithoutPropertyChanges = proceedWithoutPropertyChanges;
            await RevalidateBehaviors().WithoutChangingContext();
         }
      }

      public async Task<bool> SetValidatesTrueWhenEmpty(bool validatesTrueWhenEmpty)
      {
         if (_validatesTrueWhenEmpty != validatesTrueWhenEmpty)
         {
            _validatesTrueWhenEmpty = validatesTrueWhenEmpty;
            await RevalidateBehaviors().WithoutChangingContext();
         }

         return false;
      }

      private async Task HandleIsValidChangedTask(IResponsiveTaskParams paramDict)
      {
         await RevalidateBehaviors().WithoutChangingContext();
      }
   }
}