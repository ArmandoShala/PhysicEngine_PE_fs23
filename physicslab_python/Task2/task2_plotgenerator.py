import matplotlib.pyplot as plt
import numpy as np

def getCsvData(pathToFile):
    csvData = np.genfromtxt(pathToFile, delimiter=',', dtype=None, skip_header=0, encoding='utf-8')
    return [{'label': csvData[0, i], 'data': csvData[1:, i].astype(np.float64)} for i in range(0, csvData.shape[1])]

def plotDataOfCsv(plotData, pathToFile):
    for i in range(1, len(plotData)):
        plt.plot(plotData[0]['data'], plotData[i]['data'], label=plotData[i]['label'])

    title = pathToFile.split('_')
    typeOfAxis = title[3].split('.')[0]
    axisName = f"{getUnitOfThingAndStuff(title[3])}"
    plt.xlabel("t [s]")
    plt.ylabel(f'{typeOfAxis} [{axisName}]')
    plt.title(f'Time series of moving {title[2]} {typeOfAxis}')
    plt.legend()

    plt.grid()
    # for datatype in ['png', 'pdf', 'svg', 'eps', 'ps', 'raw', 'rgba', 'svgz', 'tif', 'tiff', 'jpg', 'jpeg', 'pgf', 'bmp', 'gif']:
    for datatype in ['png', 'svg']:
        plt.savefig(f'time_series_{pathToFile}.{datatype}')

    plt.show()

def getUnitOfThingAndStuff(type):
    if type == 'impulse':
        return 'kg * v'
    elif type == 'position':
        return 'm'
    elif type == 'velocity':
        return 'm/s'
    else:
        return 'no unit'


files = ['time_series_Wuerfel1_impulse_x.csv', 'time_series_Wuerfel1_position_x.csv',
         'time_series_Wuerfel1_velocity_x.csv', 'time_series_Wuerfel2_impulse_x.csv',
         'time_series_Wuerfel2_position_x.csv', 'time_series_Wuerfel2_velocity_x.csv',
         'time_series_Impulse both cubes_impulse_x.csv']
for file in files:
    plotDataOfCsv(getCsvData(file), file)
