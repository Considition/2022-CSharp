
# C# Algorithm submission for the Considition 2022 Competition by [CompetitiveCoders.com](https://www.competitivecoders.com)

Final place: #6 and proud winner of category "Best worst algorithm" :)

# Overview
The competition is about generating input to a REST api that returns a score. 
I used the [GeneticSharp library](https://github.com/giacomelli/GeneticSharp) to run a genetic algorithm that tried out solutions and improved based on the result. 

The only "logic" was how to set the limits on possible Gene values and how to spread out the orders. 
* The gene values are found in the GenerateGene method in [SolutionChromosome.cs](https://github.com/skarlman/Considition-2022-CSharp/blob/main/Genetic/SolutionChromosome.cs). I opted to limit possible values since running the algorithm "takes forever" when every gene is to be evaluated by posting to a REST api - which proved a winning strategy given that the API performance during the competition was next to nothing.
* To limit possible genes and search space, the orders where spread based on 5 parameters:
  - First day order based on population
  - First day order based on company budget
  - Interval to renew orders
  - Amount per renew based on population
  - Amount per renew based on initial budget


# Genetic meta-parameters
* For the performance reason I put a low population size, to mutate the genes quickly based on small number of inputs.
* EliteSelection method with 2 winners kept per generation
* Uniform Crossover
* UniformMutation

The api call is done in the Fitness function and a simple lock() is used to limit the api calls to one at a time. Play nice etc. etc.

# Notes
* A guard limits the order amount to the min of companyBudget and calculated order amount (will disrupt the function of the genetic algorithm a bit)
* Caching of api results for genes to avoid waiting on api subissions when result is known (results for a submission is deterministic)
* Api submission automatically retried, but with a linear backoff time to not overwhelm the server
* API submissions limited to one at a time to not overwhelm the server
* MapInfo hardcoded for some maps due to bugs on server during the competition
* Whole program wrapped in an endless loops with try/catch and retries (with delay) due to server errors during competition


# Running
The parameters to the program is read from text files (sorry) and is just the mapname and bagtype the program shall optimize. If no config files are present, they are created with default values automatically.

The program logs all submits and responses in raw json files, as well as a list of scores per parameter in a csv file.

During the competition a copy of the application per map and bagtype was run in parallell.




# Competition site

The competition itself and how the evaluation of the solutions work is described in more in detail on [Considition.com/rules](https://www.considition.com/rules).
