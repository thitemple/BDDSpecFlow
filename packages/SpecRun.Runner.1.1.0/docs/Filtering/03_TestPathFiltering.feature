Feature: Test Path filtering

Background: 
	Given I have a feature file with a scenario as
		"""
			Feature: Feature_1

			Scenario: Scenario_1
				When I do something

			Scenario: Scenario_2
				When I do something
			Scenario: Scenario_2postfix
				When I do something
		"""
	And I have a feature file with a scenario as
		"""
			Feature: Feature_2

			Scenario: Scenario_1
				When I do something
		"""
	And all steps are bound and pass

Scenario: Should be able to filter for feature
	Given the filter is configured to 'testpath:Feature:Feature_1'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 3     | 

Scenario: Should be able to filter for scenario
	Given the filter is configured to 'testpath:Scenario:Scenario_1'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 2     | 

Scenario: Should be able to filter for complex path
	Given the filter is configured to 'testpath:Feature:Feature_1/Scenario:Scenario_1'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 1     | 

Scenario: Should not include postfixed scenarios
	Given the filter is configured to 'testpath:Scenario:Scenario_2'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 1     | 
