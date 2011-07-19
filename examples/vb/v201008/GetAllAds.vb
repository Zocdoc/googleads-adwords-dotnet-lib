' Copyright 2011, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example retrieves all ads given an existing ad group. To add
  ''' ads to an existing ad group, run AddAds.vb.
  '''
  ''' Tags: AdGroupAdService.get
  ''' </summary>
  Class GetAllAds
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example retrieves all ads given an existing ad group. To add ads " & _
            "to an existing ad group, run AddAds.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllAds
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService(AdWordsService.v201008.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))

      ' Create a selector and set the filters.
      Dim selector As New AdGroupAdSelector
      selector.adGroupIds = New Long() {adGroupId}
      ' By default disabled ads aren't returned by the selector. To return them
      ' include the DISABLED status in the statuses field.
      selector.statuses = New AdGroupAdStatus() {AdGroupAdStatus.ENABLED, AdGroupAdStatus.PAUSED, _
          AdGroupAdStatus.DISABLED}

      Try
        Dim page As AdGroupAdPage = service.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each tempAdGroupAd As AdGroupAd In page.entries
            Console.WriteLine("Ad id is {0} and status is {1}", tempAdGroupAd.ad.id, _
                tempAdGroupAd.status)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get Ad(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace