### Feature description ###

Clearly and concisely describe the problem or feature (this cannot be empty).

### Purpose/benefits of the feature ###

### Use cases ###

### What problem it solves? ###

### Do competitors have this this feature? If possible, add comparison and also share relevant links. Anyhow, this should be available in requirements doc which you can link

### Analysis and design ###

If there is an external design, link to its project documentation area.
If there is an internal discussion on the forum, provide the link.

### Solution description ###

Describe your code changes in detail for reviewers.

### Output screenshots ###

Post the output screenshots if an UI is affected or added due to this feature.

### Areas affected and ensured ###

List the areas are affected by your code changes.

### API Changes ###

List all API changes here (or just put None), example:

Added:
 - string ListView.GroupName { get; set; } //Bindable Property
 - int ListView.GroupId { get; set; } // Bindable Property
 - void ListView.Clear ();

Changed:
 - object ListView.SelectedItem => Cell ListView.SelectedItem

### Behavioral Changes ###

Describe any non-bug related behavioral changes that may change how users app behaves when upgrading to this version of the codebase.

### Test cases ###

Provide the unit testing written file details to understand the use cases considered in this implementation.
If there is no TDD (if it’s not possible to follow), provide the UI automation script location and the Excel file that contains the use cases considered in this implementation.
Provide the test cases Excel file alone if the feature cannot be automated in any case.

List out all the scenarios you have tested after include these changes. You can refer the predefined scenarios from below link.

https://syncfusion.atlassian.net/wiki/display/CHARTXAMARIN/Chart+Test+Plan

### Testbed sample location ###

Provide the test bed sample location where code reviewers can review the new feature’s behaviors. This depends on the CI process that your team follows. It can be from NPMCI, HockeyApp, staging site, local server, etc.

### Does it have any known issues?

If this feature contains any known issues, provide the proper details about the issues.

### Does it have memory leak?

Ensure the feature contains memory leak or not (if applicable).

### Does it affect the existing CPU usage for the live demo? ###

<Live Demo>

Ensure the fix affects existing CPU usage level or not (if applicable).

### Does it affect existing memory for the performance sample? ###

<Performance Demo>

Ensure the fix affects existing memory level or not (if applicable).

### MR CheckList ###

- [ ] Have you ensured in iOS, Android, WinUI, and macOS(if supported)?
- [ ] If there is any API change, did you get approval from PLO through JIRA Tasks?
- [ ] Is there any existing behavior change of other features due to this code change?
- [ ] Have you enabled the necessary settings in your project, if you have created any new project?
- [ ] Have you suppressed any warning or binding errors?
- [ ] Did you add a sample in the testbed for your feature?
- [ ] Did you record this case in the unit test or UI test?
- [ ] Whether the new APIs and its comments are added as per standard?
- [ ] Does it contain code that reflects any internal framework API?
- [ ] Have you included license for your control(If it is stable)?
- [ ] Did you ensure the cases mentioned in this [link](https://syncfusion.sharepoint.com/sites/WinUI/SitePages/Testing-Scenarios.aspx?OR=Teams-HL&CT=1667908514796&clickparams=eyJBcHBOYW1lIjoiVGVhbXMtRGVza3RvcCIsIkFwcFZlcnNpb24iOiIyNy8yMjEwMjgwNzIwMCIsIkhhc0ZlZGVyYXRlZFVzZXIiOmZhbHNlfQ%3D%3D)?
- [ ] Did you test the memory leak with your feature?
- [ ] Did you ensure the performance? Check this link to know more about performance optimization and how to automate?
- [ ] Does it need localization? If so, did you ensure the cases mentioned in this link?
- [ ] Does it follow the design system guidelines and support light and dark themes?
- [ ] Did you ensure the new control / feature met accessibility requirements?
- [ ] Did you ensure RTL?
- [ ] If you added any interaction related code, have you used touch and gesture APIs from core project?
- [ ] If you use a third-party package, did you get approval to use it? If not, please get approval before merging.
