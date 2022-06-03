# Axon record linkage

How it works

Axon is developed to link datasets that cannot be linked on hard or even soft criteria. Axon links datasets by evaluating and then applying Bayesian relations between fields in both sets.
Axon uses a two step approach.
Step 1	The records are linked on hard criteria like city and month of birth. Each record form dataset 1 has then several candidate records in dataset 2.
Step 2	Axon derives Bayesian relations between the content in other fields like telephone numbers called, disease code in set 1 and care code in set 2 or income in set 1 and type of neighborhood in set 2. Bayesian relations are calculated between all fields in both sets.
Step 3	Axon uses the derived knowledge to match the datasets again. The matching now becomes better. The number of candidate found are now less than in step 1. Then Axon derives new estimations based on the new match and matches again. Now the match is even better and the number of candidates shrinks even more. Axon can proceed with this until there are no gains found. Mostly three to four loops are sufficient.   

Conditions

There has to be a relation between field in set 1 and set 2. If for example set 1 uses a different identity number for shops then in set 2 a relation exists. Or set 1 uses a disease coding system and set 2 a care coding system. The relation is only not yet known.
The system can handle billions of records, at least 256 fields in both sets and 32000 categories in each field. The only limit is the ram memory of your computer/server.
Axon uses a Bayesian or a Chi Square estimation. Therefore the estimation is based on categories, not on ratio’s. A field like income has to be converted to categories.

Literature

There is a lot of academic literature on this topic However the concept record linkage can apply to other linkage methods like linking on Soundex’s and distances.
Next references are about the method used in Axon:
 

