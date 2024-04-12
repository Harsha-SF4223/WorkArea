### Bug Description ###

Clearly and concisely describe the problem or feature (this cannot be empty).

### Root Cause ###

Briefly describe the root cause and analysis of the problem.
If there is an internal discussion on the forum, provide the link.

### Reason for the bug (maybe some wrong condition or wrong code)

### Why it fails because of the above reason?

### Reason for not identifying earlier ###

Find how it was missed in our earlier testing and development by analysing the below checklist. This will help prevent similar mistakes in the future. 

**Guidelines/documents are not followed**

Common guidelines / Core team guideline
Specification document
Requirement document

**Guidelines/documents are not given**

Common guidelines / Core team guideline
Specification document
Requirement document

**Reason:**
Mention any one or more reasons from the above points.

**Action taken:**
What action did you take to avoid this in future?

**Related areas:**
Is there any other related areas also to be addressed?
               
### Is Breaking issue? ###

If it is a breaking issue, provide the commit detail which caused this break.

### Solution description ###

Describe your code changes in detail for reviewers.

### Output screenshots ###

Post the output screenshots if an UI is affected or added due to this bug.

**Before changes:**
Add the image which was taken before making these changes. Place a cursor here and click "Attach a file" button and upload the image.

**After changes:**
Add the image which was taken after making these changes. Place a cursor here and click "Attach a file" button and upload the image.

### Areas affected and ensured ###

List out the areas are affected by your code changes. 
Is there any existing behavior change of other features due to this code change?

### Test cases ###

List out the test cases.

### Does it have any known issues?

If this fix contains any known issues, provide the proper details about the issues.

### Does it have memory leak?

Ensure the fix contains memory leak or not (if applicable).

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
- [ ] Did you perform the automation / manual testing against your fix?
- [ ] Did you record this case in the unit test or UI test?
- [ ] Have you suppressed any warning or binding errors?
- [ ] Is there any existing behavior change of other features due to this code change?
- [ ] Does it need localization? If so did you ensure the cases mentioned in this link?
- [ ] Whether the new APIs and its comments are added as per standard?
- [ ] Did you ensure the cases mentioned in this link?
- [ ] Did you ensure the fix (if applicable) met accessibility requirements?
- [ ] If you added any interaction related code, have you used touch and gesture APIs from core project?
- [ ] Does it contain code that reflects any internal framework API?
- [ ] Did you ensure the cases mentioned in this link?
- [ ] Did you ensure RTL?
- [ ] Did you test the memory leak with the fix?
- [ ] Did you ensure the ? Check this link to know more about performance optimization and how to automate?
- [ ] If you use a third-party package, did you get approval to use it? If not, please get approval before merging.
