/// <binding ProjectOpened='sass:watch' />
'use strict';

const del = require('del');
const gulp = require('gulp');
const gulpif = require('gulp-if');
const sass = require('gulp-dart-sass');
const autoprefixer = require('gulp-autoprefixer');
const sourcemaps = require('gulp-sourcemaps');

// Do certain stuff only in a dev build
const devBuild = (process.env.NODE_ENV || 'development').trim().toLowerCase() === 'development';

function onlyInDevBuild(input) {
    return gulpif(devBuild, input);
}

// Set-up SASS compiler
const sassOptions = {
    outputStyle: devBuild ? 'expanded' : 'compressed',

    sourceMapEmbed: devBuild,
    sourceMapContents: devBuild,
    sourceMap: devBuild,
};
sass.compiler = require('sass');

gulp.task('sass-main', function () {
    return gulp
        .src('./_scss/main.scss')
        .pipe(onlyInDevBuild(sourcemaps.init()))
        .pipe(sass(sassOptions).on('error', sass.logError))
        .pipe(autoprefixer({ cascade: false }))
        .pipe(onlyInDevBuild(sourcemaps.write()))
        .pipe(gulp.dest('./wwwroot/build/css/'));
});

gulp.task('copy-fonts', function () {
    return gulp
        .src('./node_modules/@fortawesome/fontawesome-free/webfonts/*')
        .pipe(gulp.dest('./wwwroot/build/fonts/'));
});

gulp.task('sass:watch', function () {
    gulp.watch('./_scss/**/*.scss', gulp.series('sass-main'));
});

gulp.task('clean', function (cb) {
    return del(['./wwwroot/build'], cb);
});

gulp.task('build', gulp.parallel('sass-main', 'copy-fonts'));

gulp.task('default', gulp.series('clean', 'build'));
