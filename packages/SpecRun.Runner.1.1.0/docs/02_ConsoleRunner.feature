Feature: Console runner

Background: 
	Given I have a test project 'SpecRun.TestProject'
	And I have a feature file with a scenario as
		"""
			Feature: Simple Feature
			Scenario: Simple Scenario
				Given there is something
				When I do something
				Then something should happen
		"""

Scenario: Should be able to execute a simple passing scenario
	Given all steps are bound and pass
	When I execute the tests through the console runner with
         | Setting      | Value                   |
         | TestAssembly | SpecRun.TestProject.dll |
	Then the console runner output should contain 'Total: 1'

@config
Scenario: Should be able to specify the test run configuration as a file
	Given all steps are bound and pass
	And there is a specrun configuration file 'CustomConfig.srprofile' as
		"""
		<?xml version="1.0" encoding="utf-16"?>
		<TestProfile xmlns="http://www.specrun.com/schemas/2011/09/TestProfile">
		  <TestAssemblyPaths>
			<TestAssemblyPath>SpecRun.TestProject.dll</TestAssemblyPath>
		  </TestAssemblyPaths>
		</TestProfile>
		"""
	When I execute the tests through the console runner with
		| Setting    | Value                 |
		| ConfigFile | CustomConfig.srprofile |
	Then the console runner output should contain 'Total: 1'

@config
Scenario: Should use Default.srcconfig as default configuration file
	Given all steps are bound and pass
	And there is a specrun configuration file 'Default.srprofile' as
		"""
		<?xml version="1.0" encoding="utf-16"?>
		<TestProfile xmlns="http://www.specrun.com/schemas/2011/09/TestProfile">
		  <TestAssemblyPaths>
			<TestAssemblyPath>SpecRun.TestProject.dll</TestAssemblyPath>
		  </TestAssemblyPaths>
		</TestProfile>
		"""
	When I execute the tests through the console runner with
		| Setting      | Value                   |
		| TestAssembly | SpecRun.TestProject.dll |
	Then the console runner output should contain 'Total: 1'

@config
Scenario: Should use assemblyfilename.srcconfig as default configuration file
	Given all steps are bound and pass
	And there is a specrun configuration file 'SpecRun.TestProject.dll.srprofile' as
		"""
		<?xml version="1.0" encoding="utf-16"?>
		<TestProfile xmlns="http://www.specrun.com/schemas/2011/09/TestProfile">
		  <TestAssemblyPaths>
			<TestAssemblyPath>SpecRun.TestProject.dll</TestAssemblyPath>
		  </TestAssemblyPaths>
		</TestProfile>
		"""
	When I execute the tests through the console runner with
		| Setting      | Value                   |
		| TestAssembly | SpecRun.TestProject.dll |
	Then the console runner output should contain 'Total: 1'

Scenario: Should be able to log trace to a log file
	Given all steps are bound and pass
	When I execute the tests through the console runner with
         | Setting      | Value                   |
         | TestAssembly | SpecRun.TestProject.dll |
         | LogFile      | customlog.log           |
	Then the console runner output should not contain 'When I do something'
	And there should be a file 'customlog.log' containing 'When I do something'
	And the console runner output should contain 'Total: 1'
