import React from 'react';
import axios from "axios";
import { MenuItem, FormControl, InputLabel, Button, TextField, Select, Box, ToggleButtonGroup, ToggleButton, FormControlLabel } from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import Checkbox from '@mui/material/Checkbox';
import Settings from './settings';
const dayjs = require('dayjs')
export default function AddTask() {
    const [state, setState] = React.useState({
        projects: [],
        currentAligment: 0,
        selectedProject: { name: "", id: "" },
        hasComments: false,
    })
    const ENDPOINT = Settings.ENDPOINT;

    const commentFormData = new FormData();
    const taskFormData = new FormData();

    React.useEffect(() => {
        loadData()
    }, [])

    const loadData = async () => {
        await axios.get(`${ENDPOINT}/Project/GetNameList`).then(res => {
            let mappedProjects = [];
            res.data.map(x => { mappedProjects.push({ id: x.id, name: x.name }) })
            setState({ ...state, ['projects']: mappedProjects, ['selectedProject']: mappedProjects[0] });
        })
        commentFormData.set('TaskId', null);
        commentFormData.set('CommentType', state.currentAligment);
        commentFormData.set('Text', '');
        commentFormData.set('File', '');

        taskFormData.set('TaskName', "");
        taskFormData.set('ProjectId', "");
        taskFormData.set('StartDate', dayjs().toJSON());
    }
    const changeProjectHandler = async (event) => {
        setState({ ...state, ['selectedProject']: state.projects.find(x => x.id === event.target.value) });

    }
    const handleCheckHasComment = (e) => {

        setState({ ...state, ['hasComments']: e.target.checked });
    }
    const createCommentHandler = async () => {
        commentFormData.set('CommentType', state.currentAligment);
        await axios.post(`${ENDPOINT}/Comment/`, commentFormData);
    }
    const createTaskHandler = async () => {
        taskFormData.set('ProjectId', state.selectedProject.id);

        await axios.post(`${ENDPOINT}/Task/`, taskFormData, { headers: { 'Content-Type': 'multipart/form-data', 'Access-Control-Allow-Origin': '*' } }).then(res => {
            commentFormData.set('TaskId', res.data.id);
            if (state.hasComments === true) {

                createCommentHandler();
                window.close();
            }
            window.close();

        })
    }

    return <div style={{ margin: 10, display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center' }}>
        <h4>Add Task</h4>

        <Box sx={{ minWidth: '50%' }}>
            <FormControl variant="standard" fullWidth>
                <InputLabel>Projects</InputLabel>
                <Select
                    value={state.selectedProject.id}
                    onChange={changeProjectHandler}
                >
                    {state.projects.map((x) => (
                        <MenuItem key={x.id} value={x.id}>
                            {x.name}
                        </MenuItem>
                    ))}
                </Select>

            </FormControl>
            <TextField
                autoFocus
                margin="dense"
                label="TaskName"
                type="text"
                fullWidth
                variant="standard"
                onChange={(event) => {
                    taskFormData.set('name', event.target.value)
                }}
            />
            <Box sx={{ display: "flex", flexDirection: "column", paddingTop: "20px", }}>
                <LocalizationProvider dateAdapter={AdapterDayjs} >
                    <DateTimePicker
                        renderInput={(props) => <TextField {...props} />}
                        label="Start TIme"
                        value={new dayjs()}
                        onChange={(newValue) => {
                            taskFormData.set('StartDate', dayjs(newValue).toJSON())
                        }}
                    />
                </LocalizationProvider>
            </Box>
            <FormControlLabel control={<Checkbox onChange={handleCheckHasComment} />} label="Add comment" />

            {
                state.hasComments ? <div>
                    <ToggleButtonGroup
                        fullWidth
                        color="primary"
                        exclusive
                        value={state.currentAligment}
                        onChange={(event, newAlignment) => {
                            if (newAlignment !== null) {
                                setState({ ...state, ['currentAligment']: newAlignment });
                                console.log(state);
                            }

                        }}
                        aria-label="Platform"
                    >
                        <ToggleButton value={1}>File</ToggleButton>
                        <ToggleButton value={0}>Text</ToggleButton>
                    </ToggleButtonGroup>
                    {
                        state.currentAligment === 1 ? <TextField
                            margin="dense"
                            fullWidth
                            type="file"

                            onChange={(e) => {
                                if (e.target.files.length > 0) {
                                    commentFormData.set('File', e.target.files[0], e.target.files[0].name)
                                }
                            }}
                        /> : <TextField
                            label="Description"
                            margin="dense"
                            fullWidth

                            type="text"
                            onChange={(e) => {
                                commentFormData.set('Text', e.target.value)
                            }}
                        />
                    }
                </div> : <div></div>
            }




        </Box>
        <Button type='submit' onClick={createTaskHandler} variant="outlined">Create Task</Button>
    </div>
}
