from logger import logger
from flask import Flask, request, jsonify, render_template
from flask_cors import CORS
from src import recipe_recommendation as rr
import pandas as pd
import numpy as np
import os
import config as cf
from sklearn.externals import joblib

logger = logger.load_log()
app = Flask(__name__)
app.debug = True
CORS(app)


@app.route("/api/v1/train_model", methods=['POST', 'OPTIONS'])
def train():
    try:
        path_ing_model = os.path.join(cf.BASE_PATH,'models','SGD_ing.sav')
        path_inst_model = os.path.join(cf.BASE_PATH, 'models', 'SGD_inst.sav')
        path_cuisine_model = os.path.join(cf.BASE_PATH, 'models', 'SGD_cuisine.sav')
        request_data = request.json
        train_data =request_data['train']
        df_train = pd.DataFrame(train_data)
        X = df_train['instructions']
        X1 = df_train['ingredients_NER']
        y = df_train['user_response']
        cuisine = rr.predict_cuisine(df_train['ingredients_NER'])
        if os.path.isfile(path_ing_model):
            model_ing = joblib.load(path_ing_model)
            rr.SGD_ing_train(X1, y,model_ing)
        if os.path.isfile(path_inst_model):
            model_inst = joblib.load(path_inst_model)
            rr.SGD_inst_train(X,y,model_inst)
        if os.path.isfile(path_cuisine_model):
            model = joblib.load(path_cuisine_model)
            rr.SGD_cuisine_train(cuisine,y,model)
        rr.SGD_inst_train(X,y)
        rr.SGD_ing_train(X1,y)
        rr.SGD_cuisine_train(cuisine,y)
        return "Model trained"
    except Exception as e:
        return "Failed to train the model"


@app.route("/api/v1/predict", methods=['POST', 'OPTIONS'])
def predict():
    try:
        request_data = request.json
        test_data = request_data['test_data']
        df_test = pd.DataFrame(test_data)
        pred = []
        X = list(df_test['instructions'])
        X1 = list(df_test['ingredients_NER'])
        cuisine = rr.predict_cuisine(df_test['ingredients_NER'])
        for i in range (len(X)):
            ing_pred = rr.predict_ingredients(X1[i])
            inst_pred = rr.predict_instructions(X[i])
            cuisine_pred = rr.predict_by_cuisine(cuisine[i])
            data = list((ing_pred+inst_pred+cuisine_pred)/3)
            pred.append(data.index(max(data)))
        df_test['user_response'] = pred
        return_dict = df_test.to_dict(orient='records')
        return jsonify(return_dict)
    except Exception as e:
        return "exception occured while predicting"


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8080)
