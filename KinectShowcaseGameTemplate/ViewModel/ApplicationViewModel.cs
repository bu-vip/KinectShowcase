﻿using GalaSoft.MvvmLight;
using KinectShowcase.ViewModel;
using KinectShowcaseCommon.UI_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectShowcaseGameTemplate.ViewModel
{
    public class ApplicationViewModel : ViewModelBase
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public SkeletonView SkeletonView { get; set; }

        public class ChangePageMessage
        {
            public IPageViewModel ViewModel { get; private set; }
            public ChangePageMessage(IPageViewModel aModel)
            {
                ViewModel = aModel;
            }
        }

        public ApplicationViewModel()
        {
            //Register for change view model messages
            MessengerInstance.Register<ChangePageMessage>(this, ChangeViewModelMessageRecieved);

            // Add available pages
            PageViewModels.Add(ViewModelLocator.Locator().MainViewModel);

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    RaisePropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        private void ChangeViewModelMessageRecieved(ChangePageMessage aMessage)
        {
            ChangeViewModel(aMessage.ViewModel);
        }

        #endregion
    }
}
