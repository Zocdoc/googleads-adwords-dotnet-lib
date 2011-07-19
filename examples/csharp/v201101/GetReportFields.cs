// Copyright 2011, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201101;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example gets report fields.
  ///
  /// Tags: ReportDefinitionService.getReportFields
  /// </summary>
  class GetReportFields : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets report fields.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetReportFields();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the ReportDefinitionService.
      ReportDefinitionService reportDefinitionService = (ReportDefinitionService) user.GetService(
          AdWordsService.v201101.ReportDefinitionService);

      // The type of the report to get fields for.
      // E.g.: KEYWORDS_PERFORMANCE_REPORT
      ReportDefinitionReportType reportType = (ReportDefinitionReportType) Enum.Parse(
          typeof(ReportDefinitionReportType), _T("INSERT_REPORT_TYPE_HERE"));

      try {
        // Get report fields.
        ReportDefinitionField[] reportDefinitionFields = reportDefinitionService.getReportFields(
            reportType);
        if (reportDefinitionFields != null && reportDefinitionFields.Length > 0) {
          // Display report fields.
          Console.WriteLine("The report type '{0}' contains the following fields:", reportType);

          foreach (ReportDefinitionField reportDefinitionField in reportDefinitionFields) {
            Console.Write("- {0} ({1})", reportDefinitionField.fieldName,
                reportDefinitionField.fieldType);
            if (reportDefinitionField.enumValues != null) {
              Console.Write(" := [{0}]", String.Join(", ", reportDefinitionField.enumValues));
            }
            Console.WriteLine();
          }
        } else {
          Console.WriteLine("This report type has no fields.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve fields for report type. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}