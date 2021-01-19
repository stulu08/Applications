//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"
#include <iostream>
#include <string>
#include <windows.h>
using namespace UWP_c___App;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
MainPage::MainPage()
{
	InitializeComponent();
	this->MinWidth = 1499;

}
std::string make_string(const std::wstring& wstring)
{
	auto wideData = wstring.c_str();
	int bufferSize = WideCharToMultiByte(CP_UTF8, 0, wideData, -1, nullptr, 0, NULL, NULL);
	auto utf8 = std::make_unique<char[]>(bufferSize);
	if (0 == WideCharToMultiByte(CP_UTF8, 0, wideData, -1, utf8.get(), bufferSize, NULL, NULL))
		throw std::exception("Can't convert string to UTF8");

	return std::string(utf8.get());
}
Platform::String^ convertFromString(const std::string& input)
{
	std::wstring w_str = std::wstring(input.begin(), input.end());
	const wchar_t* w_chars = w_str.c_str();

	return (ref new Platform::String(w_chars));
}

Platform::String^ generateGoogleSearchUrl(const std::string words) {
	String^ url = convertFromString("https://www.google.com/search?q=" + words);
	
	return url;
}


void UWP_c___App::MainPage::Search(Platform::Object^ sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs^ e)
{
	if (e->Key == Windows::System::VirtualKey::Enter) {
		String^ link = input->Text;
		if (make_string(link->Data()).rfind("https://", 0) == 0|| make_string(link->Data()).rfind("http://", 0)==0)
		{
			
			browser->Navigate(ref new Uri(input->Text));
			ViewGrid_PageStart->Visibility = Windows::UI::Xaml::Visibility::Collapsed;
			View_Grid_Loading->Visibility = Windows::UI::Xaml::Visibility::Visible;
			ProgressRing_Loading->IsActive = true;

		}
		else {
			browser->Navigate(ref new Uri(generateGoogleSearchUrl(make_string(link->Data()))));
			ViewGrid_PageStart->Visibility = Windows::UI::Xaml::Visibility::Collapsed;
			View_Grid_Loading->Visibility = Windows::UI::Xaml::Visibility::Visible;
			ProgressRing_Loading->IsActive = true;
		}

	}
}


void UWP_c___App::MainPage::browser_Loading(Windows::UI::Xaml::FrameworkElement^ sender, Platform::Object^ args)
{
	
	
}


void UWP_c___App::MainPage::browser_Unloaded(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
	browser->Navigate(ref new Uri("https://stulu08.github.io/cpp_browser/sites/html/start.html"));
}
void UWP_c___App::MainPage::browser_LoadCompleted(Platform::Object^ sender, Windows::UI::Xaml::Navigation::NavigationEventArgs^ e)
{
	Back_Button->IsEnabled = true;
	ProgressRing_Loading->IsActive = false;
	browser->Visibility = Windows::UI::Xaml::Visibility::Visible;
	View_Grid_Loading->Visibility = Windows::UI::Xaml::Visibility::Collapsed;
	input->Text = browser->BaseUri->Domain;
	
	

}
void UWP_c___App::MainPage::Back_Button_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
	if (browser->CanGoBack)
		browser->GoBack();
	else
		Back_Button->IsEnabled = false;
}
