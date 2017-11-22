-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

DROP TABLE recipe
DROP TABLE tags
DROp Table category
drop table website_users
drop table user_recipes
drop table user_plan
drop table recipe_tags
drop table recipe_category
drop table plan_recipes
drop table plans
drop table website_users;
drop table app_user;
drop table meal;
drop table meal_plan;
drop table meal_recipe;
drop table message;
drop table recipe;



BEGIN;

-- CREATE statements go here

--RECIPE
CREATE TABLE recipe
(
recipe_id int identity not null,
recipe_name varchar(1000) not null,
directions varchar(max) not null,
publics int not null, --0 private (false), 1 pbulic (true)
ingredients varchar(max) not null, 
image_name varchar(max) not null,
approved int DEFAULT 0 not null, --0 not approved, 1 approved

constraint pk_recipe_id primary key (recipe_id)
);

--TAGS
CREATE TABLE tags
( --
tag_id int identity not null,
tag_name varchar(256) not null,

constraint pk_tag_id primary key (tag_id)
);

--CATEGORY
CREATE TABLE category
(
category_id int identity not null,
category_name varchar(256) not null,

constraint pk_category_id primary key (category_id)
);

--WEBSITE USER
CREATE TABLE website_users
(
users_id int identity not null,
users_name  varchar(24) not null,
password varchar(max) not null, 
email varchar(48) not null,
authorization_level int not null,
salt varchar(max),

constraint pk_users_id primary key (users_id)
);

--USER RECIPES
CREATE TABLE user_recipes
(
users_id int not null,
users_name varchar(24) not null,
recipe_id int not null,
recipe_name varchar(1000) not null, 

constraint fk_user_recipes_user_id foreign key (users_id) REFERENCES website_users (users_id) 
--constraint fk_user_recipes_user_name foreign key (user_name) REFERENCES website_user (user_name)
);



--RECIPE TAGS
CREATE TABLE recipe_tags
(
recipe_id int not null,
tag_id int not null,

Constraint pk_recipe_id_tag_id primary key(recipe_id, tag_id)
);

--RECIPE CATEGORY
CREATE TABLE recipe_category
(
recipe_id int not null,
category_id int not null,

Constraint pk_recipe_id_category_id primary key (recipe_id, category_id)
);

--PLAN
CREATE TABLE plans
(
plan_id int identity not null, 
plan_name varchar(max) not null, 

CONSTRAINT pk_plan_id PRIMARY KEY (plan_id)
);

--PLAN RECIPES
CREATE TABLE plan_recipes
(
plan_id int not null,
users_id int not null, 
category_id int not null, 
recipe_id int not null, 
days_of_week varchar(10) not null, 

CONSTRAINT fk_plan_recipes_plan_id FOREIGN KEY (plan_id) REFERENCES plans (plan_id),
--Constraint fk_plan_recipes_plan_id foreign key (plan_id) REFERENCES user_plan (plan_id) 
);

--USER PLAN
CREATE TABLE user_plan
(
plan_id int not null,
users_id int not null,

constraint fk_user_plan_plan_id FOREIGN KEY (plan_id) REFERENCES plans (plan_id),
constraint fk_user_plan_user_id FOREIGN KEY (users_id) REFERENCES website_users (users_id)
);

--MEAL
CREATE TABLE meal
(
meal_id int identity not null, 
day_of_week varchar(10) not null, 
meal_name varchar(10) not null, 

constraint pk_meal_id primary key (meal_id),
--constraint fk
);

CREATE TABLE meal_plan
(
plan_id int not null, 
meal_id int not null, 
);

CREATE TABLE meal_recipe
(
meal_id int not null, 
recipe_id int not null, 
);
COMMIT;