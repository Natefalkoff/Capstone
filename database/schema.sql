-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

BEGIN;

-- CREATE statements go here

CREATE TABLE Recipe
(
recipe_id int identity not null,
recipe_name varchar(1000) not null,
directions varchar(max) not null,
user_id varchar(24) not null, 
publics int not null,
ingredients varchar(max) not null,
image varbinary(max) not null,

constraint pk_recipe_id primary key (recipe_id)
);

CREATE TABLE tags
(
tag_id int identity not null,
tag_name varchar(256) not null,

constraint pk_tag_id primary key (tag_id)
);

CREATE TABLE category
(
category_id int identity not null,
category_name varchar(256) not null,

constraint pk_category_id primary key (category_id)
);

CREATE TABLE website_user
(
user_id int identity not null,
user_name  varchar(24) not null,
password varchar(48) not null, 
email varchar(48) not null,
authorization_level int not null,

constraint pk_user_id primary key (user_id)
);

CREATE TABLE user_recipes
(
user_id int not null,
recipe_id int not null,

constraint pk_user_id_recipe_id primary key (user_id, recipe_id)
);

CREATE TABLE meal_plan
(
plan_id int identity not null,
user_id int not null,

constraint pk_plan_id_user_id primary key (plan_id, user_id)
);

CREATE TABLE recipe_tags
(
recipe_id int not null,
tag_id int not null,

Constraint pk_recipe_id_tag_id primary key(recipe_id, tag_id)
);

CREATE TABLE recipe_category
(
recipe_id int not null,
category_id int not null,

Constraint pk_recipe_id_category_id primary key (recipe_id, category_id)
);

CREATE TABLE plan_receipes
(
plan_id int not null,
recipe_id int not null, 
days_of_week varchar(10) not null, 

Constraint pk_plan_id_receipe_id primary key (plan_id, recipe_id)
);
COMMIT;