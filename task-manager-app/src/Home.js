import axios from "axios";
import Tasks from './Tasks';
import ProjectTasksFilterOptions from './Models/TaskTableOptions'
import React from 'react';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import TextField from '@mui/material/TextField';
import { Box } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import Button from '@mui/material/Button';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import { Link } from "react-router-dom";
import Settings from "./Models/settings";
const dayjs = require('dayjs')


export default function Home() {

  const [tasks, setTasks] = React.useState([]);
  const [options, setOptions] = React.useState(new ProjectTasksFilterOptions('', ''));
  const [spendedTime, setSpendedTime] = React.useState("00:00");
  const [projects, setProjects] = React.useState([]);

  const ENDPOINT = Settings.ENDPOINT;
  console.log(ENDPOINT);
  const getDataHandler = async () => {
    let tasksPromise = axios.post(`${ENDPOINT}/Task/GetTable`, options);
    let projPromise = axios.get(`${ENDPOINT}/Project/GetNameList`);
    Promise.all([tasksPromise, projPromise]).then(values => {

      setTasks(values[0].data.tasks);
      setProjects(values[1].data);
      setSpendedTime(values[0].data.summaryTime)
    });
  }
  React.useEffect(() => {
    getDataHandler();
  }, [options])

  const updateTasks = React.useCallback(
    (() => {
      getDataHandler();
    }));

  const changeProjIdCallback = projId => {

    setOptions(data => {
      const updateOpts = { ...data };
      updateOpts.ProjectId = projId;
      return updateOpts;
    });
  }
  const changeDateCallback = date => {
    setOptions(data => {
      const updateOpts = { ...data };
      updateOpts.FromDate = date;
      return updateOpts;
    });
  }
  const removeTaskCallback = taskId => {
    let arr = [...tasks];
    const objWithIdIndex = arr.findIndex((obj) => obj.id === taskId);
    arr.splice(objWithIdIndex, 1);
    setTasks(arr);

  }

  return tasks.length !== undefined ? (

    <div className="Home">
      <header className="Home-header">
      </header>
      <Box sx={{
        display: 'flex',
        flexDirection: 'column',
        margin: '100px'
      }}>
        <Box textAlign='center'>
          <Button variant="contained">

            < Link to={'/addTask'} target="_blank" onClick={() => localStorage.setItem("updateTasksHandler", getDataHandler)}>Add Task</Link >
          </Button>
        </Box >
        <Box textAlign='center'>
          <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
            <InputLabel>Projects</InputLabel>
            <Select
              value={''}
              onChange={(e) => changeProjIdCallback(e.target.value)}
            >
              {projects.map((x) => (
                <MenuItem key={x.id} value={x.id}>
                  {x.name}
                </MenuItem>
              ))}
            </Select>

          </FormControl>
        </Box>

        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker
            displayStaticWrapperAs="desktop"
            label="Date&Time picker"
            inputFormat="DD/MM/YYYY HH:mm:ss"
            mask={"__/__/____ __:__:__"}
            format={"DD-MM-YYYY"}
            views={["day", "month", "year"]}
            onChange={(value) => { changeDateCallback(dayjs(value).toDate()); }}
            renderInput={(props) => <TextField {...props} />}
          />
        </LocalizationProvider>
        <Tasks tasks={tasks} time={spendedTime} changeProjIdCallback={changeProjIdCallback} removeTaskCallback={removeTaskCallback} updateTasksCallback={updateTasks} />
      </Box>

    </div >
  ) : <div>Downloading</div>




}

