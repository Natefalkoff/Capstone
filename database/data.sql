-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;
SELECT * FROM recipe
SELECT * FROM tags
SELECT * FROM website_users
SELECT * FROM category
SELECT * FROM user_recipes
SELECT * FROM meal
SELECT * FROM meal_plan

DELETE FROM meal_plan WHERE meal_id = '5'

--RECIPE
	INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name, approved) VALUES ('Roasted Cauliflower Dressed Up','1. 1 head of cauliflower cut into small florets\n2. 1/3 cup olive oil\n3. Salt & pepper to taste\n4. 1 shallot, cut crosswise into thin rings\n5. 1 tbsp. white wine\n6. 1 tbsp. capers, rinsed and roughly chopped\n7. Grated zest of lemon\n8. Handful of flat leaf parsley roughly chopped', 1,'3 cups of cooked and cooled spinach, finely chopped\n3 cups of bread crumbs\n1 cup of grated parmesan\n4 eggs lightly beaten\n1/2 cup softened butter\nItalian parsley\nsalt and pepper', '51143020_parmesan-roasted-cauliflower_1x1.jpg', 1);
	INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name, approved) VALUES ('Spinach Dip Balls','1. Mix all ingredients in a large bowl until well blended\n2. To prepare and serve immediately, roll level tablespoons full of the mixture into balls about the size of walnuts,\nand place on a lightly oiled cooking sheet\n3. Bake at 350 degrees for 10 minutes or until lightly browned and firm enough to pick up', 1,'3 cups cooked and cooled spinach, finely chopped\n3 cups of bread crumbs\n1 cup grated parmesan\n4 eggs lightly beaten\n1/2 cup butter, softened\nItalian parsley\nsalt and pepper', 'Spinach-Balls-Appetizer-Recipe-9-e1451920507572.jpg', 1);
	INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name, approved) VALUES ('Strawberry Yogurt Popsicles', '1. To a food process add strawberries\n2. Add 3 tbl. maple syrup, lemon juice and mix till you get a smooth puree\n3. In a bowl, whisk together geek yogurt with 1 tbl. maple syrup and vanilla extract\n4. Fill each popsicle mold with 2-3 tsp of the strawberry puree\n5. Then place 1-2 tbl of the Greek yogurt mixture on top. And finally again add few tsp the strawberry puree\ntill your mold is full\n6. Place the popsicle mold in the freezer for 2 hours.', 1,'10 ounce strawberries ( 2 cups)\n4 tbl. maple syrup\n3/4 tsp lemon juice\n1 cup Greek yogurt\n1/2 tsp vanilla extract', 'strawberry-greek-yogurt-popsicles.jpg', 1);
	INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name, approved) VALUES ('Guacamole','1. Peel and pit avocadoes\n2. Dice onion\n3. Chop fresh cilantro\n4. Dice roma tomatoes\n5. Mince garlic\n6. In a medium bowl, mash together the avocados, lime juic, and salt\n7. Mix in onion, cilantro, tomatoes, and garlic\n8. Stir in cayenne pepper',1, '3 avocados, peeled, pitted, and mashed\n1 lime, juiced\n1 tsp salt\n1/2 cup diced onion\n3 tbl. chopped fresh cilantro\n2 roma tomatoes diced\n1 tsp. minced garlic\n1 pinch ground cayenne pepper\n1 pinch ground cayenne pepper','Best-Guacamole-Ever-(2).jpg', 1 );
	INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name, approved) VALUES ('Red Rice Salad with Peaches and Cucumber', '1. Peel and dice 4 cups of cucumbers\n2. Rinse Bhutanese red rice\n3. Grate fresh ginger\n4. Peel and dice fresh peaches\n5. Toast sesame seeds\n6. Chop fresh mint\n7. Seed and mince fresh serrano chilies\n8. Coarsely chop arugula\n9. Toss cucumber in a bowl with 3/4 teaspoon of salt and let stand in the water for 1 hour\n10. Rinse and drain in sieve, ensuring to press out all of the excess water\n11. Combine water, rice and 1/4 teaspoon of salt in a large sauce pan\n12. Bring to boil over high heat. Reduce heat to maintain a gentle simmer and cover and cook until the rice\nis tender about 20 minutes\n13. Whisk lemon juice, oil, ginger and the remaining 1/4 teaspoon of salt in a large bowl until well combined\n14. Add the cucumber, the rice, peaches, sunflower seeds, mint and chili\n15. Mix well and allow to chill down for 30 minutes | 16. Add arugula and toss just before serving', 1,'4 cups diced English cucumbers (about 1¼ pounds)\n1¼ teaspoons salt, divided\n1½ cups water\n1 cup Bhutanese red rice, rinsed\n⅓ cup fresh lemon juice\n3 tablespoons extra-virgin olive oil\n1 tablespoon grated fresh ginger\n3 cups diced ripe but firm peaches or nectarines (about 1 pound)\n¼ cup sunflower seeds, toasted (see Tip)\n¼ cup chopped fresh mint\n1 teaspoon minced seeded fresh serrano chile, or to taste\n3 cups arugula, coarsely chopped', '3758370.jpg', 1);
	INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name, approved) VALUES ('Chicken and Kale Hand Pies', '1. Preheat oven to 425 degrees\n2. Prepare two baking sheets with parchment paper\n3. On a cool surface, roll out one disc of pie dough to a 14 inch round.\nDust with flour first, if needed, to prevent sticking\n4. With a knife or biscuit cutter, cut out six 4 1/4 inch circles, re-rolling dough just once if needed\n5. Transfer cut dough on parchment to a baking sheet. Repeat with remaining dough, cutting out six larger, 4 1/4 inch rounds\n6. Place sheet in the fridge until ready to use\n7. In a large skillet over medium-high heat, melt butter. Add leek and sauté until soft, about 3 minutes\n8. Add kale, thyme, salt, and pepper, to the skillet an sauté until kale wilts, about 3-5 minutes\n9. Sprinkle flour over mixture. Stir to combine. Add broth and bring to a boil.\nCook stirring often, until mixture thickens, about 2 minutes\n10. Transfer to a medium bowl, season with more salt and pepper if desired, and stir in chicken. Let cool slightly\n11. Remove rounds from fridge\n12. Place a rounded 1/4 cup of chicken mixture on each of the smaller dough rounds, leaving a 1/2 inch border.\nBrush edges with egg and top with larger dough rounds. Using fingers, press edges firmly to seal\n13. Cut a small vent in each pie. Bake until browned and crisp, 30 minutes, rotating sheet halfway through\n14. Let cook slightly on sheets that have been set on a wire rack. Serve warm or at room temperature.', 1,'2 discs pie dough, either home made or purchased\n2 tbsp. all -purpose flour\nSalt & pepper to taste\n2 tbsp. unsalted butter\n1 leek, white and light green parts only, halved lengthwise,\n  cut crosswise 1/4 inch thick and rinsed well\n1 small bunch kale, de-ribbed and coarsely chopped\n1 tsp fresh thyme leaves\n1/4 tsp dried sage\n1 cup chicken or vegetable broth\n1 cup cooked chicken or turkey, shredded into bite size pieces\n1 large egg, lightly beaten', '54ef8bf9f04cb_-_chicken-sweet-potato-pot-pies-recipe-wdy0313-xl.jpg', 1);

	-- INSERT statements go here \n

	--TAGS
	INSERT INTO tags (tag_name) VALUES ('Gluten Free');
	INSERT INTO tags (tag_name) VALUES ('Low Sodium');
	INSERT INTO tags (tag_name) VALUES ('High Fiber');
	INSERT INTO tags (tag_name) VALUES ('Soy Free');
	INSERT INTO tags (tag_name) VALUES ('Dairy Free');
	INSERT INTO tags (tag_name) VALUES ('High in Iron');

	--CATEGORY
	INSERT INTO category (category_name) VALUES ('Appetizers');
	INSERT INTO category (category_name) VALUES ('Breakfast');
	INSERT INTO category (category_name) VALUES ('Casseroles');
	INSERT INTO category (category_name) VALUES ('Chicken');
	INSERT INTO category (category_name) VALUES ('Dinner');
	INSERT INTO category (category_name) VALUES ('Fish');
	INSERT INTO category (category_name) VALUES ('Holidays & Occasions');
	INSERT INTO category (category_name) VALUES ('Lunch');
	INSERT INTO category (category_name) VALUES ('Pasta');
	INSERT INTO category (category_name) VALUES ('Salads');
	INSERT INTO category (category_name) VALUES ('Snacks');	
	INSERT INTO category (category_name) VALUES ('Soups');
	
	--WEBSITE USER
	INSERT INTO website_users (users_name, password, email, authorization_level, salt, signup)VALUES ('rob', 'y+bjtdQQpBEu7dSYeSH4aEhPF/E=', 'rob@rob', 3, 'sdZUcw2JQTw=', 1);
	INSERT INTO website_users (users_name, password, email, authorization_level, salt, signup)VALUES('cam', 'NQ6AdbvWgCEBChpx5IvUh531zfg=', 'cam@cam', 3, 'cQJa/1TRWAo=', 1);
	INSERT INTO website_users (users_name, password, email, authorization_level, salt, signup)VALUES ('nate', '636Pu+UykDhsFckNyBIXrodpZ+o=', 'nate@nate', 3, '+TN6kD82RPw=', 1);

--USER RECIPES

iNSERT INTO user_recipes (users_id, recipe_id,) VALUES (users_id, recipe_id);

--USER PLAN
INSERT INTO user_plan (users_id) VALUES (users_id);

--RECIPE TAGS
INSERT INTO recipe_tags (recipe_id, tag_id) VALUES (tag_id);

--RECIPE CATEGORY
INSERT INTO recipe_category (recipe_id, category_id) VALUES (recipe_id, category_id);

--PLAN RECIPES

INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Sunday');
INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Monday');
INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Tuesday');
INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Wednesday');
INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Thursday');
INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Friday');
INSERT INTO plan_recipes (plan_id, users_id, category_id, recipe_id, days_of_week) VALUES (plan_id, users_id, category_id, recipe_id, 'Saturday');
--INSERT INTO plan_recipes (recipe_id, days_of_week) VALUES (DAYOFWEEK);


COMMIT;