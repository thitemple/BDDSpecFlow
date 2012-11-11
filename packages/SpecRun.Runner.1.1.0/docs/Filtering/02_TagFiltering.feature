Feature: Tag filtering

Background: 
	Given I have a feature file with a scenario as
		"""
			Feature: Simple Feature
			@tag1
			Scenario: Tagged scenario
				When I do something

			Scenario: Non-Tagged scenario
				When I do something

			@tag2
			Scenario: Tagged scenario with other tag
				When I do something

			@tag2 @tag3
			Scenario: Second tagged scenario with other tag
				When I do something
		"""
	And all steps are bound and pass

Scenario: Should be able to include tagged scenarios
	Given the filter is configured to 'tag:tag1'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 1     | 

Scenario: Should be able to include tagged features
	Given I have a feature file with a scenario as
		"""
			@featuretag
			Feature: Tagged Feature

			Scenario: Scenario1
				When I do something

			Scenario: Scenario2
				When I do something
		"""
	And the filter is configured to 'tag:featuretag'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 2     | 

Scenario: Should be able to use @ for tag filtering
	Given the filter is configured to '@tag1'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 1     | 

Scenario: Should be able to filter scenario outline examples
	Given I have a feature file with a scenario as
		"""
			Feature: Tagged Feature

			Scenario Outline: Scenario1
				When I do something with <key>

			@sotag1
			Examples:
			| key          |
			| tag1 example |

			@sotag2
			Examples:
			| key          |
			| tag2 example |
		"""
	And the filter is configured to 'tag:sotag1'
	When I execute the tests
	Then the execution summary should contain
		| Total | 
		| 1     | 

