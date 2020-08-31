using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using RomanNumerals.Core;

namespace RomanNumerals.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private MainViewModel mainViewModel = new MainViewModel();
        private TextInputEditText arabicEditText;
        private TextInputEditText romanNumeralEditText;
        private TextView currentErrorTextView;
        private Button convertToRomanButton;
        private Button convertToArabicButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            romanNumeralEditText = FindViewById<TextInputEditText>(Resource.Id.mainview_romannumeraledittext);
            arabicEditText = FindViewById<TextInputEditText>(Resource.Id.mainview_arabicnumeraledittext);

            convertToRomanButton = FindViewById<Button>(Resource.Id.mainview_converttoroman);
            convertToArabicButton = FindViewById<Button>(Resource.Id.mainview_converttoarabic);

            currentErrorTextView = FindViewById<TextView>(Resource.Id.mainview_currenterror);

            convertToRomanButton.Click += ConvertToRomanButton_Click;
            convertToArabicButton.Click += ConvertToArabicButton_Click;

            mainViewModel = new MainViewModel();
        }

        private void ConvertToArabicButton_Click(object sender, EventArgs e)
        {
            mainViewModel.RomanNumeral = romanNumeralEditText.Text;
            mainViewModel.RomanToArabic();
            arabicEditText.Text = mainViewModel.ArabicNumeral.ToString();
            currentErrorTextView.Text = mainViewModel.CurrentError;
        }

        private void ConvertToRomanButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(arabicEditText.Text, out var arabic))
            {
                mainViewModel.ArabicNumeral = arabic;
                mainViewModel.ArabicToRoman();
                romanNumeralEditText.Text = mainViewModel.RomanNumeral;
                currentErrorTextView.Text = mainViewModel.CurrentError;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
