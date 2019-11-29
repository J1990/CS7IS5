from sklearn.feature_extraction.text import CountVectorizer, TfidfVectorizer
from sklearn.metrics import accuracy_score, confusion_matrix, classification_report
from sklearn.pipeline import Pipeline
from sklearn.feature_extraction.text import TfidfTransformer
from sklearn.linear_model import LogisticRegression,SGDClassifier
from sklearn.externals import joblib
from nltk.corpus import stopwords
import os
import numpy as np
import re
import string
import config as cf

import pickle
import os
path_ing_vectorizer = os.path.join(cf.BASE_PATH,'models','vectorizer_ing.pkl')
path_inst_vectorizer = os.path.join(cf.BASE_PATH,'models','vectorizer_inst.pkl')
path_cuisine = os.path.join(cf.BASE_PATH,'models','cuisine.pkl')
with open(path_inst_vectorizer, 'rb') as fid:
     vectorizer_inst =pickle.load(fid)

with open(path_ing_vectorizer, 'rb') as fid:
    vectorizer_ing = pickle.load(fid)

with open(path_cuisine, 'rb') as fid:
    vectorizer_cuisine,cuisine_predictor,cuisine_encoder = pickle.load(fid)
punctuations = string.punctuation

REPLACE_BY_SPACE_RE = re.compile('[/(){}\[\]\|@,;]')
BAD_SYMBOLS_RE = re.compile('[^0-9a-z #+_]')
STOPWORDS = set(stopwords.words('english'))

def clean_str(s):
    """Clean sentence"""
    if type(s) is float:
        return s
    if len(s)<1:
        return s
    s = re.sub(r"[^A-Zæåøa-z0-9(),!\??\'\`]", " ", s)
    s = re.sub(r"\'s", " \'s", s)
    s = re.sub(r"\'ve", " \'ve", s)
    s = re.sub(r"n\'t", " n\'t", s)
    s = re.sub(r"\'re", " \'re", s)
    s = re.sub(r"\'d", " \'d", s)
    s = re.sub(r"\'ll", " \'ll", s)
    s = re.sub(r",", " , ", s)
    s = re.sub(r"!", " ! ", s)
    s = re.sub(r"\(", " \( ", s)
    s = re.sub(r"\)", " \) ", s)
    #s = re.sub(r"\?", " \? ", s)
    s = re.sub(r"\s{2,}", " ", s)
    s = re.sub(r'\S*(x{2,}|X{2,})\S*',"xxx", s)
    # s = nb_tokenizer(s)
    return s

def clean_text(text):
    """
        text: a string

        return: modified initial string
    """
    if type(text) is float:
        return " "
    text = clean_str(text)
    text = text.lower()  # lowercase text
    text = REPLACE_BY_SPACE_RE.sub(' ', text)  # replace REPLACE_BY_SPACE_RE symbols by space in text
    text = BAD_SYMBOLS_RE.sub('', text)  # delete symbols which are in BAD_SYMBOLS_RE from text
    text = ' '.join(word for word in text.split() if word not in STOPWORDS)  # delete stopwors from text
    return text

def data_clean(df):
    df['instructions'] = df['instructions'].apply(clean_text)
    return df

def train_model(X_train, y_train):
    logreg = Pipeline([('vect', CountVectorizer()),
                    ('tfidf', TfidfTransformer()),
                    ('clf', LogisticRegression(n_jobs=1, C=1e5)),
                   ])
    logreg.fit(X_train, y_train)
    #save the model to disk
    filename = os.path.join(cf.BASE_PATH, "models", "logreg.sav")
    joblib.dump(logreg, filename)

def SGD_ing_train(X,y,SGD=None):
  X = vectorizer_ing.transform(X)
  n_iter = 1000
  if SGD == None:
    SGD = SGDClassifier(loss='log')
  for i in range(n_iter):
    SGD.partial_fit(X,y,classes=[0,1])
  filename = os.path.join(cf.BASE_PATH, "models", "SGD_ing.sav")
  joblib.dump(SGD, filename)

def SGD_inst_train(X,y,SGD=None):
  X = vectorizer_inst.transform(X)
  n_iter = 1000
  if SGD == None:
    SGD = SGDClassifier(loss='log')
  for i in range(n_iter):
    SGD.partial_fit(X,y,classes=[0,1])
  filename = os.path.join(cf.BASE_PATH, "models", "SGD_inst.sav")
  joblib.dump(SGD, filename)

def SGD_cuisine_train(X, y, SGD=None):
  n_iter = 1000
  X= X.reshape(-1,1)
  if SGD == None:
      SGD = SGDClassifier(loss='log')
  for i in range(n_iter):
      SGD.partial_fit(X, y, classes=[0,1])
  filename = os.path.join(cf.BASE_PATH, "models", "SGD_cuisine.sav")
  joblib.dump(SGD, filename)

def predict_instructions(X):
    filename = os.path.join(cf.BASE_PATH, "models", "SGD_inst.sav")
    #logger.info("Loading file from {}". format(filename))
    # load the model from disk
    X = clean_text(X)
    model = joblib.load(filename)
    X = [X]
    X = vectorizer_inst.transform(X)
    y_pred = model.predict_proba(X)
    return y_pred[0]

def predict_ingredients(X):
    filename = os.path.join(cf.BASE_PATH, "models", "SGD_ing.sav")
    #logger.info("Loading file from {}". format(filename))
    # load the model from disk
    X = clean_text(X)
    model = joblib.load(filename)
    X = [X]
    X = vectorizer_ing.transform(X)
    y_pred = model.predict_proba(X)
    return y_pred[0]

def predict_by_cuisine(X):
    filename = os.path.join(cf.BASE_PATH, "models", "SGD_cuisine.sav")
    #logger.info("Loading file from {}". format(filename))
    # load the model from disk
    model = joblib.load(filename)
    X = [X]
    X = np.array(X).reshape(-1,1)
    y_pred = model.predict_proba(X)
    return y_pred[0]

def predict_cuisine(X):
    X = vectorizer_cuisine.transform(X)
    y_pred = cuisine_predictor.predict(X)
    return y_pred