// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example illustrates how to update an ad group, setting its
  /// status to 'PAUSED'. To create an ad group, run AddAdGroup.cs.
  ///
  /// Tags: AdGroupService.mutate
  /// </summary>
  public class UpdateAdGroup : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new UpdateAdGroup();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to update an ad group, setting its status to " +
            "'PAUSED'. To create an ad group, run AddAdGroup.cs";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201109_1.AdGroupService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

      // Create the ad group.
      AdGroup adGroup = new AdGroup();
      adGroup.status = AdGroupStatus.PAUSED;
      adGroup.id = adGroupId;

      // Create the operation.
      AdGroupOperation operation = new AdGroupOperation();
      operation.@operator = Operator.SET;
      operation.operand = adGroup;

      try {
        // Update the ad group.
        AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroup pausedAdGroup = retVal.value[0];
          writer.WriteLine("Ad group with id = '{0}' was successfully updated.",
              pausedAdGroup.id);
        } else {
          writer.WriteLine("No ad groups were updated.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to update ad group.", ex);
      }
    }
  }
}
