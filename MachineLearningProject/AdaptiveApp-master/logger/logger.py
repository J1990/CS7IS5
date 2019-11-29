import os
import json
import logging
import logging.config

import config as cf

# load the logging configuration
log_path = os.path.join(cf.BASE_PATH, "config", "logging.json")

def load_log():
    with open(log_path, 'r') as logging_configuration_file:
        config_dict = json.load(logging_configuration_file)

    config_dict["handlers"]["file_handler"]["filename"] = os.path.join(cf.BASE_PATH, "logs", "app.log")
    config_dict["handlers"]["error_file_handler"]["filename"] = os.path.join(cf.BASE_PATH, "logs", "errors.log")

    logging.config.dictConfig(config_dict)

    # Log that the logger was configured
    logger = logging.getLogger(__name__)
    return logger
