-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;
SELECT * FROM recipe
SELECT * FROM tags
SELECT * FROM website_user
SELECT * FROM category

--RECIPE
INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name) VALUES ('Roasted Cauliflower Dressed Up', '1. 1 head of cauliflower cut into small florets | 2. 1/3 cup olive oil | 3. Salt & pepper to taste | 4. 1 shallot, cut crosswise into thin rings | 5. 1 tbsp. white wine | 6. 1 tbsp. capers, rinsed and roughly chopped | 7. Grated zest of lemon | 8. Handful of flat leaf parsley roughly chopped', 1, '3 cups of cooked and cooled spinach, finely chopped, | 3 cups of bread crumbs, | 1 cup of grated parmesan, | 4 eggs lightly beaten, | 1/2 cup softened butter, | Italian parsley, | salt and pepper', 'Cooked Cauliflower picture');
INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name) VALUES ('Spinach Dip Balls', '1. Mix all ingredients in a large bowl until well blended, | 2. To prepare and serve immediately,  roll level tablespoons full of the mixture into balls about the size of walnuts, and place on a lightly oiled cooking sheet. | 3. Bake at 350 degrees for 10 minutes or until lightly browned and firm enough to pick up', 1, '3 cups cooked and cooled spinach, finely chopped | 3 cups of bread crumbs | 1 cup grated parmesan | 4 eggs lightly beaten | 1/2 cup butter, softened | Italian parsley, | salt and pepper', 'spinach picture');
INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name) VALUES ('Strawberry Yogurt Popsicles', '1. To a food process add strawberries | 2. Add 3 tbl. maple syrup, lemon juice and mix till you get a smooth puree | 3. In a bowl, whisk together geek yogurt with 1 tbl. maple syrup and vanilla extract | 4. Fill each popsicle mold with 2-3 tsp of the strawberry puree. | 5. Then place 1-2 tbl of the Greek yogurt mixture on top. And finally again add few tsp the strawberry puree till your mold is full | 6. Place the popsicle mold in the freezer for 2 hours.', 1, '10 ounce strawberries ( 2 cups) | 4 tbl. maple syrup | 3/4 tsp lemon juice | 1 cup Greek yogurt | 1/2 tsp vanilla extract', 'Strawberry Yogurt Popsicles picture');
INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name) VALUES ('Guacamole','1. Peel and pit avocadoes | 2. Dice onion | 3. Chop fresh cilantro | 4. Dice roma tomatoes | 5. Mince garlic | 6. In a medium bowl, mash together the avocados, lime juic, and salt | 7. Mix in onion, cilantro, tomatoes, and garlic. | 8. Stir in cayenne pepper',1, '3 avocados, peeled, pitted, and mashed | 1 lime, juiced | 1 tsp salt | 1/2 cup diced onion | 3 tbl. chopped fresh cilantro | 2 roma tomatoes diced | 1 tsp. minced garlic | 1 pinch ground cayenne pepper | 1 pinch ground cayenne pepper','Guacamole picture' );
INSERT INTO recipe (recipe_name, directions, publics, ingredients, image_name) VALUES ('Red Rice Salad with Peaches and Cucumber', '1. Peel and dice 4 cups of cucumbers | 2. Rinse Bhutanese red rice | 3. Grate fresh ginger | 4. Peel and dice fresh peaches | 5. Toast sesame seeds | 6. Chop fresh mint | 7. Seed and mince fresh serrano chilies | 8. Coarsely chop arugula | 9. Toss cucumber in a bowl with 3/4 teaspoon of salt and let stand in the water for 1 hour | 10. Rinse and drain in sieve, ensuring to press out all of the excess water | 11. Combine water, rice and 1/4 teaspoon of salt in a large sauce pan | 12. Bring to boil over high heat. Reduce heat to maintain a gentle simmer and cover and cook until the rice is tender about 20 minutes | 13. Whisk lemon juice, oil, ginger and the remaining 1/4 teaspoon of salt in a large bowl until well combined. | 14. Add the cucumber, the rice, peaches, sunflower seeds, mint and chili. | 15. Mix well and allow to chill down for 30 minutes | 16. Add arugula and toss just before serving', 2, '4 cups diced English cucumbers (about 1¼ pounds) | 1¼ teaspoons salt, divided | 1½ cups water |  1 cup Bhutanese red rice, rinsed |  ⅓ cup fresh lemon juice | 3 tablespoons extra-virgin olive oil | 1 tablespoon grated fresh ginger | 3 cups diced ripe but firm peaches or nectarines (about 1 pound) | ¼ cup sunflower seeds, toasted (see Tip) | ¼ cup chopped fresh mint | 1 teaspoon minced seeded fresh serrano chile, or to taste | 3 cups arugula, coarsely chopped', 'Red Rice Salad with Peaches and Cucumber picture');

-- INSERT statements go here \n

--TAGS
INSERT INTO tags (tag_name) VALUES ('Gluten Free');
INSERT INTO tags (tag_name) VALUES ('Low Sodium');
INSERT INTO tags (tag_name) VALUES ('High Fiber');
INSERT INTO tags (tag_name) VALUES ('Soy Free');
INSERT INTO tags (tag_name) VALUES ('Dairy Free');
INSERT INTO tags (tag_name) VALUES ('High in Iron');

--CATEGORY
INSERT INTO category (category_name) VALUES ('Breakfast');
INSERT INTO category (category_name) VALUES ('Lunch');
INSERT INTO category (category_name) VALUES ('Dinner');
INSERT INTO category (category_name) VALUES ('Snacks');
INSERT INTO category (category_name) VALUES ('Appetizers');
INSERT INTO category (category_name) VALUES ('Salads');
INSERT INTO category (category_name) VALUES ('Soups');
INSERT INTO category (category_name) VALUES ('Casseroles');
INSERT INTO category (category_name) VALUES ('Chicken');
INSERT INTO category (category_name) VALUES ('Fish');
INSERT INTO category (category_name) VALUES ('Pasta');
INSERT INTO category (category_name) VALUES ('Holidays & Occasions');

--WEBSITE USER
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES ('CSpinks', 'cspinks', 'cspinks@techelevator.com', 1);
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES('RRankin','rrankin', 'rrankin@techelevator.com', 1);
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES ('NFalkoff', 'nfalkoff', 'nfalkoff@techelevator.com', 1);
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES ('trufflebutter', 'trufflebutter', 'trufflebutter@gmail.com', 2);
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES ('jango', 'jango','jango@gmail.com', 2);
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES('hoboDan', 'hobodan', 'hobodan@gmail.com', 3);
INSERT INTO website_user (user_name, password, email, authorization_level)VALUES ('batman', 'batman', 'batman@gmail.com', 3);

--USER RECIPES
iNSERT INTO user_recipes (user_id, recipe_id) VALUES (user_id, recipe_id);

--USER PLAN
INSERT INTO user_plan (user_id) VALUES (user_id);

--RECIPE TAGS
INSERT INTO recipe_tags (recipe_id, tag_id) VALUES (tag_id);

--RECIPE CATEGORY
INSERT INTO recipe_category (recipe_id, category_id) VALUES (recipe_id, category_id);

--PLAN RECIPES
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Sunday');
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Monday');
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Tuesday');
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Wednesday');
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Thursday');
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Friday');
INSERT INTO plan_recipes (plan_id, recipe_id, days_of_week) VALUES (plan_id, recipe_id, 'Saturday');
--INSERT INTO plan_recipes (recipe_id, days_of_week) VALUES (DAYOFWEEK);


COMMIT;