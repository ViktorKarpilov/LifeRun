import sys
import logging

def main():
    try:

        logging.basicConfig(format='%(asctime)s - %(message)s', level=logging.INFO)
        logging.info(sys.version)

    except ImportError as error:
        
        logging.error(f'Import: {error}')
        
    except Exception as exception:

        logging.error(f'Exception: {exception}')


    
        

if __name__ == "__main__":
    main()